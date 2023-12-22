using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using UAParser;
using YB_StaffingSupervisor.DataAccess.Entities;
using YB_StaffingSupervisor.DataAccess.UnitOfWork;

namespace YB_StaffingSupervisor.Common
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataProtector _protector;
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _service;
        /// <summary>  
        /// Initializes a new instance of the <see cref="LogActionFilterAttribute" /> class.  
        /// </summary>  
        /// <param name="logger">The logger.</param>  
        public LogActionFilterAttribute(IUnitOfWork service, IHttpContextAccessor httpContextAccessor, IDataProtectionProvider protectionProvider, IOptions<AppSettings> appsettings = null)
        {
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appsettings.Value;
            _protector = protectionProvider.CreateProtector(_appSettings.ProtectorValue);
            _service = service;
        }

        /// <summary>  
        /// Called when [action executing].  
        /// </summary>  
        /// <param name="context">The filter context.</param>  
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Log("OnActionExecuting", context.RouteData, context.HttpContext.Request);
            base.OnActionExecuting(context);
        }

        /// <summary>  
        /// Called when [action executed].  
        /// </summary>  
        /// <param name="context"></param>  
        /// <inheritdoc />  
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Log("OnActionExecuted", context.RouteData, context.HttpContext.Request);
            base.OnActionExecuted(context);
        }

        /// <summary>  
        /// Logs the specified method name.  
        /// </summary>  
        /// <param name="methodName">Name of the method.</param>  
        /// <param name="routeData">The route data.</param>  
        public long Log(string methodName, RouteData routeData, HttpRequest request)
        {
            var areaName = routeData.Values["area"];
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var url = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(request.HttpContext.Request);
            var IPAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var s = request.HttpContext.Session;
            string UserID = s.GetString("UserId");
            long res = 0;
            if (!string.IsNullOrEmpty(UserID))
            {
                string TokenID = s.GetString("JWToken");

                var userAgent = request.Headers["User-Agent"];
                string uaString = Convert.ToString(userAgent[0]);
                var uaParser = Parser.GetDefault();
                ClientInfo c = uaParser.Parse(uaString);

                UserLogModel userLog = new UserLogModel
                {
                    UserId = UserID == string.Empty ? null : _protector.Unprotect(UserID),
                    Url = url,
                    Area = (string)(areaName ?? null),
                    Controller = (string)controllerName,
                    Action = (string)actionName,
                    Parameter = string.Empty,
                    IpAddress = IPAddress,
                    Token = TokenID,
                    Browser = Convert.ToString(c.UA),
                    OS = Convert.ToString(c.OS),
                    Device = Convert.ToString(c.Device)
                };
                res = _service.UserLogRepository.SaveUserActivityLog(userLog);
            }
            return res;
        }
    }
}
