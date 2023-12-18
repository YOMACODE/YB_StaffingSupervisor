using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YB_StaffingSupervisor.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EncryptedActionParameterAttribute : ActionFilterAttribute
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataProtector _protector;
        private readonly AppSettings _appSettings;
        private const string PARAMETER_NAME = "V2";
        public ValidatedURLExtensions validatedURLExtensions = new ValidatedURLExtensions();
        /// <summary>  
        /// Initializes a new instance of the <see cref="LogActionFilterAttribute" /> class.  
        /// </summary>  
        /// <param name="logger">The logger.</param>  
        public EncryptedActionParameterAttribute(IHttpContextAccessor httpContextAccessor, IDataProtectionProvider protectionProvider, IOptions<AppSettings> appsettings = null)
        {
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appsettings.Value;
            _protector = protectionProvider.CreateProtector(_appSettings.ProtectorValue);
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UriHelper.GetEncodedUrl(filterContext.HttpContext.Request).Contains("?"))
            {
                string contextQuery = validatedURLExtensions.GetAbsoluteUri(filterContext.HttpContext).Query.ToString();
                if (contextQuery.Contains(PARAMETER_NAME))
                {
                    //var enc = validatedURLExtensions.GetAbsoluteUri(filterContext.HttpContext).Query.ToString();
                    var enc = filterContext.HttpContext.Request.Query[PARAMETER_NAME];
                    enc = _protector.Unprotect(enc);
                    QueryString queryString = new QueryString(enc);
                    filterContext.HttpContext.Request.QueryString = queryString;
                }
                else if(filterContext.HttpContext.Request.Method == "GET")
                {
                    if (contextQuery != "")
                    {
                        string encryptedQuery = _protector.Protect(contextQuery);
                        string redirectToPagePath = filterContext.HttpContext.Request.Path.Value + "?" + PARAMETER_NAME + "=" + encryptedQuery;
                        filterContext.HttpContext.Response.Redirect(redirectToPagePath);
                    }
                }
            }            
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
