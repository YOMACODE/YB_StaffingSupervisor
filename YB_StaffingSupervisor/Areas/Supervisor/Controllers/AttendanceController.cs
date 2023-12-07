using ClosedXML.Excel;
using iTextSharp.text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
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

            TeamAttendanceCustom teamAttendanceCustom = new TeamAttendanceCustom();
            SearchRequest.SupervisorId = _dataProtector.Unprotect(baseModel.UserId);
            teamAttendanceCustom = await _service.AttendanceRepository.GetDailyTeamAttendanceListing(SearchRequest);
            if (teamAttendanceCustom != null && teamAttendanceCustom.quickAttendanceListing != null)
            {
                teamAttendanceCustom.quickAttendanceListing.ToList().ForEach(c =>
                {
                    c.DailyAttendanceId = _dataProtector.Protect(c.DailyAttendanceId);
                    c.UserId = _dataProtector.Protect(c.UserId);
                });
            }
            if (teamAttendanceCustom != null && teamAttendanceCustom.selfieBasedAttendanceListing != null)
            {
                teamAttendanceCustom.selfieBasedAttendanceListing.ToList().ForEach(c =>
                {
                    c.DailyAttendanceId = _dataProtector.Protect(c.DailyAttendanceId);
                    c.UserId = _dataProtector.Protect(c.UserId);
                });
            }

            return View(teamAttendanceCustom);
        }
        [HttpGet]
        public async Task<IActionResult> AttendanceRequests([FromQuery] AttendanceRequestCustom SearchRequest, string sortOrder, string sortColumn, string pagesize, int page = 1)
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Logout", "Home", new { area = "" }).WithWarning("Warning !", "Unauthorized Access.");
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchUserCode))
            {
                ViewBag.SearchUserCode = SearchRequest.SearchUserCode;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchAttendanceFrom))
            {
                ViewBag.SearchAttendanceFrom = SearchRequest.SearchAttendanceFrom;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchAttendanceTo))
            {
                ViewBag.SearchAttendanceTo = SearchRequest.SearchAttendanceTo;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchStatusType))
            {
                ViewBag.SearchStatusType = SearchRequest.SearchStatusType;
            }
            if (sortOrder == "desc")
            {
                ViewData["sortOrder"] = "asce";
            }
            else
            {
                ViewData["sortOrder"] = "desc";
            }
            ViewData["sortOrderForPagination"] = sortOrder;
            SearchRequest.SortColumnName = sortColumn ?? String.Empty;
            SearchRequest.SortOrderBy = (sortOrder == null || sortOrder == "desc") ? "desc" : "asce";
            if (!string.IsNullOrWhiteSpace(SearchRequest.SortColumnName))
            {
                ViewData["sortColumnName"] = SearchRequest.SortColumnName;
            }
            int PageSize;
            if (pagesize == null)
            {
                PageSize = 10;
            }
            else if (sortColumn != string.Empty)
            {
                PageSize = Convert.ToInt32(pagesize);
            }
            else
            {
                PageSize = Convert.ToInt32(pagesize);
            }
            ViewBag.page = page;
            ViewBag.PageSize = PageSize;

            AttendanceRequestCustom attendanceRequestCustom = new AttendanceRequestCustom();
            SearchRequest.SupervisorId = _dataProtector.Unprotect(baseModel.UserId);

            attendanceRequestCustom = await _service.AttendanceRepository.GetAttendanceRequestListing(page, PageSize, SearchRequest);
            if (attendanceRequestCustom != null && attendanceRequestCustom.attendanceListing != null)
            {
                attendanceRequestCustom.attendanceListing.ToList().ForEach(c =>
                {
                    c.DailyAttendanceId = _dataProtector.Protect(c.DailyAttendanceId);
                    c.UserId = _dataProtector.Protect(c.UserId);
                    //c.ClientId = _dataProtector.Protect(c.ClientId);
                    //c.BusinessSelectionId = _dataProtector.Protect(c.BusinessSelectionId);
                });
            }

            return View(attendanceRequestCustom);
        }

        [HttpGet]
        public async Task<IActionResult> AttendanceLogs(UserAttendanceCustom SearchRequest)
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Logout", "Home", new { area = "" }).WithWarning("Warning !", "Unauthorized Access.");
            }

            if (string.IsNullOrEmpty(SearchRequest.UserId))
            {
                return RedirectToAction("Index", "MyDashboard", new { area = "Dashboard" }).WithWarning("Warning !", "Invalid user.");
            }

            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchMonth))
            {
                ViewBag.SearchMonth = SearchRequest.SearchMonth;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchYear))
            {
                ViewBag.SearchYear = SearchRequest.SearchYear;
            }

            UserAttendanceCustom userAttendanceCustom = new UserAttendanceCustom();
            SearchRequest.SupervisorId = _dataProtector.Unprotect(baseModel.UserId);
            SearchRequest.UserId = _dataProtector.Unprotect(SearchRequest.UserId);
            userAttendanceCustom = await _service.AttendanceRepository.GetUserAttendanceListing(SearchRequest);
            if (userAttendanceCustom != null && userAttendanceCustom.attendanceListing != null)
            {
                userAttendanceCustom.attendanceListing.ToList().ForEach(c =>
                {
                    c.DailyAttendanceId = _dataProtector.Protect(c.DailyAttendanceId);
                    c.UserId = _dataProtector.Protect(c.UserId);
                });
            }
            userAttendanceCustom.monthModelsListing = new SelectList(DateTimeFormatInfo.InvariantInfo.MonthNames.Where(m => !String.IsNullOrEmpty(m)).Select((monthName, index) => new SelectListItem { Value = (index + 1).ToString(), Text = monthName }), "Value", "Text");
            userAttendanceCustom.yearModelsListing = new SelectList(Enumerable.Range(DateTime.Today.Year - 10, 20).Select(x => new SelectListItem() { Text = x.ToString(), Value = x.ToString() }), "Value", "Text");
            userAttendanceCustom.UserId = _dataProtector.Protect(SearchRequest.UserId);
            return View(userAttendanceCustom);
        }
        [HttpPost]
        public async Task<IActionResult> ApproveRejectAttendance(string AttendanceRequestId, string ApproveRejectStatus, string ApproveRejectComment, string Token)
        {
            string msg = "";
            try
            {
                var routeValues = ControllerContext.HttpContext.Request.RouteValues;
                var url = $"/{routeValues["area"]}/{routeValues["controller"]}/{routeValues["action"]}";
                bool checkToken = _loginUserRepo.ValidateCurrentToken(Token, url);
                if (checkToken == false)
                {
                    return RedirectToAction("Logout", "Home").WithWarning("Warning !", "Unauthorized Access.");
                }
                else
                {
                    if (!string.IsNullOrEmpty(AttendanceRequestId))
                    {
                        long result = await _service.AttendanceRepository.AttendanceVerification(_dataProtector.Unprotect(AttendanceRequestId), ApproveRejectStatus, ApproveRejectComment, _dataProtector.Unprotect(baseModel.UserId));
                        if (result == 1)
                        {
                            if (ApproveRejectStatus == "Approve")
                            {
                                msg = "Approved successfully.";
                            }
                            else if (ApproveRejectStatus == "Reject")
                            {
                                msg = "Rejected successfully.";
                            }
                        }
                        else if (result == -2)
                        {
                            msg = "Invalid Status";
                        }
                        else if (result == -1)
                        {
                            msg = "attendance not found";
                        }
                        else
                        {
                            msg = "Something went wrong,Please try again.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(new { msg });

        }
        [HttpPost]
        public IActionResult GetAttendanceMeetingMapViewComponent(string AttendanceDate, string UserId, string Token)
        {
            var routeValues = ControllerContext.HttpContext.Request.RouteValues;
            var url = $"/{routeValues["area"]}/{routeValues["controller"]}/{routeValues["action"]}";
            bool checkToken = _loginUserRepo.ValidateCurrentToken(Token, url);
            if (checkToken == false)
            {
                return RedirectToAction("Logout", "Home").WithWarning("Warning !", "Unauthorized Access.");
            }
            else
            {
                return ViewComponent("AttendanceMeetingMap", new { attendanceDate = AttendanceDate, userId = UserId });
            }
        }

        #region Attendance Request Report

        [HttpGet]
        public async Task<IActionResult> AttendancerequestReport(string userid, string YomaId, string AttendenceFrom, string AttendenceTo, string status)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                userid = _dataProtector.Unprotect(baseModel.UserId);
                dt = await _service.AttendanceRepository.GetAttendanceRequestExport(userid, YomaId, AttendenceFrom, AttendenceTo, status);
                //Do Export for 
                if (dt != null && dt.Rows.Count > 0)
                {
                    var totalrowcount = dt.Rows.Count;
                    var totalcolcount = dt.Columns.Count;
                    //dt.Columns.Remove("DisplayOrder");
                    ds.Tables.Add(dt.Copy());

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheetPFFormat = wb.Worksheets.Add("Attendance Report");

                        sheetPFFormat.FirstRow().FirstCell().InsertTable(dt);
                        sheetPFFormat.Columns().AdjustToContents();
                        sheetPFFormat.FirstRow().Style.Font.Bold = true;

                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            return File(MyMemoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AttendanceRequestReport.xlsx");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("AttendanceRequests", "Attendance").WithInfo("Info !", "No data found!");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("AttendanceRequests", "Attendance").WithDanger("Error !", "Something went wrong.please try again.");
            }
        }
        #endregion
    }
}
