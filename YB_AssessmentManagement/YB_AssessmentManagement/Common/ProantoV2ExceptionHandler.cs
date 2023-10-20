using System;
using System.Data;
using System.Text.Json;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using YB_AssessmentManagement.DataAccess.Entities;
using YB_AssessmentManagement.DataAccess.UnitOfWork;

namespace YB_AssessmentManagement.Common
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ProantoRecruitementV2ExceptionHandler: ExceptionFilterAttribute
    {
        private readonly IUnitOfWork _service;
        public ProantoRecruitementV2ExceptionHandler(IUnitOfWork service)
        {
            _service = service;
        }
        public override void OnException(ExceptionContext filterContext)
        {            
            try
            {
                //string ExceptionMessage = Convert.ToString(filterContext.Exception.Message) ?? string.Empty;
                string ExceptionStackTrack = Convert.ToString(filterContext.Exception.StackTrace) ?? string.Empty;
                string ControllerName = Convert.ToString(filterContext.RouteData.Values["controller"]) ?? string.Empty;
                string ActionName = Convert.ToString(filterContext.RouteData.Values["action"]) ?? string.Empty;
                string currentArea = filterContext.RouteData.DataTokens["area"] != null ? Convert.ToString(filterContext.RouteData.DataTokens["area"]) : string.Empty;
                var UserId = filterContext.HttpContext.Session.GetString("UserId"); //s.GetString("UserId");

                //Application Type : Live Or Test
                var applicationType = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["ApplicationType"];

                ErrorLogModel errorLogModel = new ErrorLogModel
                {
                    ApplicationType = applicationType,
                    ExceptionMessage = JsonSerializer.Serialize(new { message = filterContext.Exception.Message }),
                    Area = currentArea,
                    Controller = ControllerName,
                    Action = ActionName,
                    ExceptionStackTrack = ExceptionStackTrack,
                    UserId= UserId
                };
                long res = _service.ErrorLogRepository.SaveErrorLog(errorLogModel);
            }
            catch (Exception)
            {

            }

            filterContext.ExceptionHandled = true;
            filterContext.Result = new RedirectResult("~/Error/Error404");
            base.OnException(filterContext);
        }
    }
}
