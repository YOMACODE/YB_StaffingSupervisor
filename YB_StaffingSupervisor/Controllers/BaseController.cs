using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using YB_StaffingSupervisor.DataAccess.Entities;
using YB_StaffingSupervisor.DataAccess.UnitOfWork;
using YB_StaffingSupervisor.LoginRepository.ILoginRepository;
using YB_StaffingSupervisor.Models;

namespace YB_StaffingSupervisor.Controllers
{
    public class BaseController : Controller
    {
        private readonly IDataProtector _dataProtector;
        private readonly IConfiguration _configuration;
        public IConfiguration Configuration { get; }

        public string UserID;
        public BaseModel baseModel = new BaseModel();
        public readonly IUnitOfWork _service;
        private readonly ILoginUserRepository _loginUserRepo;
        private readonly AppSettings _appSettings;
        public BaseController(IUnitOfWork service, IDataProtectionProvider dataProtectionProvider, ILoginUserRepository loginUserRepo = null, IOptions<AppSettings> appsettings = null)
        {
            #region Configuration Builder add the appsetting.json key and value         
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
            _configuration = Configuration;
            #endregion

            #region DataProtection
            _appSettings = appsettings.Value;
            _dataProtector = dataProtectionProvider.CreateProtector(_appSettings.ProtectorValue);
            #endregion

            #region UnitOfWork
            _service = service;
            _loginUserRepo = loginUserRepo;
            #endregion
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var s = HttpContext.Session;
            if (string.IsNullOrWhiteSpace(s.GetString("UserId")))
            {
                if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    // filterContext.HttpContext.Response.StatusCode = 403;
                    // filterContext.Result = new JsonResult("/SessionTimeOut/Index");
                }
                else
                {
                    //  filterContext.Result = new RedirectResult("~/SessionTimeOut/Index");
                }
                // HttpContext.Session.Clear();
                return;
            }
            UserID = s.GetString("UserId") != null ? _dataProtector.Unprotect(s.GetString("UserId")) : String.Empty;

            #region Filter use to find the clientId from manage client Module to bind ClientId in partial button click
            if (filterContext.HttpContext.Request.Query.ContainsKey("ClientId"))
            {
                baseModel.PartialClientId = Convert.ToString(filterContext.HttpContext.Request.Query["ClientId"]);
            }
            #endregion

            if (string.IsNullOrEmpty(s.GetString("JWToken")))
            {
                if (!string.IsNullOrEmpty(baseModel.UserId))
                {
                    _loginUserRepo.ClearToken(Convert.ToInt64(_dataProtector.Unprotect(baseModel.UserId)));
                }
                #region Maintain the ReturnUrl

                filterContext.Result = new RedirectResult("~/Home/Index?ReturnUrl=" + (filterContext.HttpContext.Request.GetEncodedPathAndQuery()));
                #endregion
                return;
            }
            else if (_loginUserRepo != null)
            {
                var routeValues = ((ControllerBase)filterContext.Controller).ControllerContext.ActionDescriptor.RouteValues;
                var url = $"/{routeValues["area"]}/{routeValues["controller"]}/{routeValues["action"]}";
                bool loginUser;
                if (filterContext.HttpContext.Request.Headers.ContainsKey("JWToken"))
                {
                    loginUser = _loginUserRepo.ValidateCurrentToken(filterContext.HttpContext.Request.Headers["JWToken"].ToString(), url);
                }
                else
                {
                    loginUser = _loginUserRepo.ValidateCurrentToken(s.GetString("JWToken"), url);
                }

                if (loginUser == false)
                {
                    TempData["Message"] = "Unauthorized Access.";
                    if (HttpContext.Session.GetString("UserId") != null)
                    {
                        _loginUserRepo.ClearToken(Convert.ToInt64(_dataProtector.Unprotect(HttpContext.Session.GetString("UserId"))));
                    }
                     //HttpContext.Session.Clear();


                    if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                         filterContext.HttpContext.Response.StatusCode = 403;
                         filterContext.Result = new JsonResult("/SessionTimeOut/Index");
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult("~/SessionTimeOut/Index");

                    }
                    return;
                }
            }

            #region Bind User Setting Model in BaseModel            
            _ = new UserSettingModel();
            UserSettingModel userSettingModel = _service.UserRepository.UserSetting(Convert.ToInt32(UserID));
            if (userSettingModel != null)
            {
                baseModel.ClientId = _dataProtector.Protect(userSettingModel.ClientId);
                baseModel.ClientName = userSettingModel.ClientName;
                baseModel.IsBoxedLayoutEnabled = userSettingModel.IsBoxedLayoutEnabled;
                baseModel.IsCollapseSidebarEnabled = userSettingModel.IsCollapseSidebarEnabled;
                baseModel.IsFixedSidebarEnabled = userSettingModel.IsFixedSidebarEnabled;
                baseModel.IsFixedHeaderEnabled = userSettingModel.IsFixedHeaderEnabled;
                baseModel.WebThemeCode = userSettingModel.WebThemeCode;
            }
            #endregion

            #region Bind UserModel in BaseModel
            _ = new UserModel();
            UserModel user = _service.UserRepository.User(Convert.ToInt32(UserID));
            if (user != null)
            {
                baseModel.UserId = _dataProtector.Protect(UserID);
                baseModel.UserName = user.UserName;
                baseModel.RoleName = user.RoleName;
                baseModel.RegionName = user.RegionName;
                baseModel.IsAdmin = user.IsAdmin;
                baseModel.CountryId = user.CountryId;
                baseModel.SiteURL = Convert.ToString(_configuration.GetSection("SiteURL").GetValue<string>("URL"));
                baseModel.TokenValue = s.GetString("JWToken");
                baseModel.MobileNumber = _dataProtector.Protect(user.MobileNumber);
                ViewData["baseModel"] = baseModel;
            }
            #endregion  
            base.OnActionExecuting(filterContext);
        }
    }
}
