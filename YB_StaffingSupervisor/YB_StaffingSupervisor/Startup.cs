using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Infrastructure;
using YB_StaffingSupervisor.DataAccess.UnitOfWork;
using YB_StaffingSupervisor.LoginRepository.ILoginRepository;
using YB_StaffingSupervisor.LoginRepository;
using YB_StaffingSupervisor.Middleware;
using YB_StaffingSupervisor.Services;
using YB_StaffingSupervisor.Common;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace YB_StaffingSupervisor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddScoped<ILoginUserRepository, LoginUserRepository>();

            
            services.AddScoped<ISessionService, SessionService>();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddMemoryCache();
            services.AddDistributedMemoryCache();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration["AppSettings:myIssuer"],
                    ValidAudience = Configuration["AppSettings:myAudience"],
                    ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(Configuration["AppSettings:ExpiryTime"])),
                };
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            {
                o.ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(Configuration["AppSettings:ExpiryTime"]));
                o.LoginPath = new PathString("/Home/Index");
                o.LogoutPath = new PathString("/Home/Logout");
                o.SlidingExpiration = true;
                o.Events = new CookieAuthenticationEvents();
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.AddSession(options =>
            {
                //Set a short timeout for easy testing.
                //if someone not working one page from some time ideal timeout will call as per the Appsetting.json
                //and user will redirect to Home page(login page)
                options.IdleTimeout = TimeSpan.FromMinutes(Convert.ToDouble(Configuration["AppSettings:ExpiryTime"]));
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.CacheProfiles.Add("DefaultNoCacheProfile", new CacheProfile
                {
                    NoStore = true,
                    Location = ResponseCacheLocation.None
                });
                options.Filters.Add(new ResponseCacheAttribute
                {
                    CacheProfileName = "DefaultNoCacheProfile"
                });
                options.Filters.Add<ProantoRecruitementV2ExceptionHandler>();
                options.Filters.Add(typeof(LogActionFilterAttribute));

            }).AddRazorOptions(options =>
            {
                options.ViewLocationFormats.Add("/{0}.cshtml");
            }).AddSessionStateTempDataProvider().SetCompatibilityVersion(CompatibilityVersion.Latest);

            _ = services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            //services.AddScoped<LogActionFilterAttribute>();

            //Inject ConnectionFactory,
            services.AddScoped<IConnectionFactory, ConnectionFactory>();

            //Inject UnitofWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 60000000;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error/Error404");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            var culture = CultureInfo.CreateSpecificCulture("en-GB");
            var dateformat = new DateTimeFormatInfo
            {
                ShortDatePattern = "dd/MM/yyy",
                LongDatePattern = "dd/MM/yyy hh:mm:ss tt"
            };
            culture.DateTimeFormat = dateformat;
            var supportedCultures = new[]
            {
                culture
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            //Multiple Query String Encrypt and Decrypt in Middleware
            //Work based on IHttpContextAccessor and IActionContextAccessor
            static bool QueryStringRequest(HttpContext context) => context.Request.QueryString.ToString().StartsWith("/?");
            app.UseWhen(context => QueryStringRequest(context), appBuilder =>
            {
                app.UseEncryptDecryptQueryStringsMiddleware();
            });


            app.UseCookiePolicy();
            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            //app.UseAuthorization();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/{controller=Admin}/{action=Index}/{id?}");
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
