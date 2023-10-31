using iTextSharp.text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using YB_StaffingSupervisor.Common;
using YB_StaffingSupervisor.Controllers;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;
using YB_StaffingSupervisor.DataAccess.UnitOfWork;
using YB_StaffingSupervisor.LoginRepository.ILoginRepository;

namespace YB_StaffingSupervisor.Areas.Supervisor.Controllers
{
    [Area("Supervisor")]
    public class AttendanceController : BaseController
    {
        private readonly IDataProtector _dataProtector;
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoginUserRepository _loginUserRepo;
        public AttendanceController(IUnitOfWork unitOfWork, IDataProtectionProvider protectionProvider, ILoginUserRepository loginUserRepo, IOptions<AppSettings> appsettings = null) : base(unitOfWork, protectionProvider, loginUserRepo, appsettings)
        {
            _appSettings = appsettings.Value;
            _dataProtector = protectionProvider.CreateProtector(_appSettings.ProtectorValue);
            _unitOfWork = unitOfWork;
            _loginUserRepo = loginUserRepo;
        }
        [HttpGet]
        public async Task<IActionResult> TeamAttendance([FromQuery] TeamAttendanceCustom SearchRequest)
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Logout", "Home", new { area = "" }).WithWarning("Warning !", "Unauthorized Access.");
            }

            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchDate))
            {
                ViewBag.SearchDate = SearchRequest.SearchDate;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchUserCode))
            {
                ViewBag.SearchUserCode = SearchRequest.SearchUserCode;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchFullName))
            {
                ViewBag.SearchFullName = SearchRequest.SearchFullName;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchMobileNumber))
            {
                ViewBag.SearchMobileNumber = SearchRequest.SearchMobileNumber;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchEmailId))
            {
                ViewBag.SearchEmailId = SearchRequest.SearchEmailId;
            }
            TeamAttendanceCustom attendanceCustom = new TeamAttendanceCustom();
            SearchRequest.SupervisorId = _dataProtector.Unprotect(baseModel.UserId);
            //attendanceCustom = await _service.AttendanceRepository.GetDailyAttendanceListing(SearchRequest);
            //if (attendanceCustom != null && attendanceCustom.quickAttendanceListing != null)
            //{
            //    attendanceCustom.quickAttendanceListing.ToList().ForEach(c =>
            //    {
            //        c.DailyAttendanceId = _dataProtector.Protect(c.DailyAttendanceId);
            //    });
            //}
            //if (attendanceCustom != null && attendanceCustom.selfieBasedAttendanceListing != null)
            //{
            //    attendanceCustom.selfieBasedAttendanceListing.ToList().ForEach(c =>
            //    {
            //        c.DailyAttendanceId = _dataProtector.Protect(c.DailyAttendanceId);
            //    });
            //}

            return View(attendanceCustom);
        }
        [HttpGet]
        public async Task<IActionResult> AttendanceRequests([FromQuery] AttendanceRequestCustom SearchRequest)
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Logout", "Home", new { area = "" }).WithWarning("Warning !", "Unauthorized Access.");
            }

            
            return View(SearchRequest);
        }
        
        [HttpGet]
        public async Task<IActionResult> AttendanceLogs(UserAttendanceCustom SearchRequest)
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Logout", "Home", new { area = "" }).WithWarning("Warning !", "Unauthorized Access.");
            }

            return View(SearchRequest);
        }
    }
}
