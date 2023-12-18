using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using YB_StaffingSupervisor.Common;

namespace YB_StaffingSupervisor.Middleware
{
    public class EncryptDecryptQueryStrings
    {
        private readonly AppSettings _appSettings;
        private const string PARAMETER_NAME = "YOMAv2";
        //private static IHttpContextAccessor _httpContextAccessor;IHttpContextAccessor httpContextAccessor,
        private readonly IDataProtector _protector;
        private readonly RequestDelegate _next;
        public ValidatedURLExtensions validatedURLExtensions = new ValidatedURLExtensions();
        public EncryptDecryptQueryStrings(IDataProtectionProvider provider, RequestDelegate next, IOptions<AppSettings> appsettings = null)
        {
            // _httpContextAccessor = httpContextAccessor;
            _appSettings = appsettings.Value;
            _protector = provider.CreateProtector(_appSettings.ProtectorValue);
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (UriHelper.GetEncodedUrl(context.Request).Contains("?"))
            {
                //Get Current URL
                string contextQuery = validatedURLExtensions.GetAbsoluteUri(context).Query.ToString();
                if (contextQuery.Contains(PARAMETER_NAME))
                {
                    var enc = context.Request.Query[PARAMETER_NAME];
                    try
                    {
                        enc = _protector.Unprotect(enc);
                        QueryString queryString = new QueryString(enc);
                        context.Request.QueryString = queryString;
                        await _next.Invoke(context);
                    }
                    catch (Exception ex)
                    {
                        var msg = ex.Message;
                        //if some one manuplate encode URL query string, 
                        //Query string not match against the cryptography key, 
                        //it will simple redirect to logout action
                        context.Response.Redirect("/Home/Logout");
                    }
                }
                else if (context.Request.Method == "GET")
                {
                    if (contextQuery != "")
                    {
                        string encryptedQuery = _protector.Protect(contextQuery);
                        string redirectToPagePath = context.Request.Path.Value + "?" + PARAMETER_NAME + "=" + encryptedQuery;
                        context.Response.Redirect(redirectToPagePath);
                    }
                }
            }
            else
            {
                await _next.Invoke(context);
            }
            //await _next.Invoke(context);
        }
    }

    public static class QueryStringMiddlewareExtensions
    {
        public static IApplicationBuilder UseEncryptDecryptQueryStringsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EncryptDecryptQueryStrings>();
        }
    }
}
