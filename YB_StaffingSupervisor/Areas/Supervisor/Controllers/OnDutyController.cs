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
    public class OnDutyController : BaseController
    {
        private readonly IDataProtector _dataProtector;
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoginUserRepository _loginUserRepo;
        public OnDutyController(IUnitOfWork unitOfWork, IDataProtectionProvider protectionProvider, ILoginUserRepository loginUserRepo, IOptions<AppSettings> appsettings = null) : base(unitOfWork, protectionProvider, loginUserRepo, appsettings)
        {
            _appSettings = appsettings.Value;
            _dataProtector = protectionProvider.CreateProtector(_appSettings.ProtectorValue);
            _unitOfWork = unitOfWork;
            _loginUserRepo = loginUserRepo;
        }

        [HttpGet]
        public async Task<IActionResult> OnDutyRequests([FromQuery] OnDutyRequestCustom SearchRequest, string sortOrder, string sortColumn, string pagesize, int page = 1)
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

            OnDutyRequestCustom onDutyRequestCustom = new OnDutyRequestCustom();
            SearchRequest.SupervisorId = _dataProtector.Unprotect(baseModel.UserId);
            onDutyRequestCustom = await _service.OnDutyRepository.GetOnDutyListing(page, PageSize, SearchRequest);
            if (onDutyRequestCustom != null && onDutyRequestCustom.onDutyListing != null)
            {
                onDutyRequestCustom.onDutyListing.ToList().ForEach(c =>
                {
                    c.OnDutyRequestId = _dataProtector.Protect(c.OnDutyRequestId);
                });
            }
            return View(onDutyRequestCustom);
        }
        [HttpPost]
        public async Task<IActionResult> ApproveRejectOnDuty(string OnDutyRequestId, string ApproveRejectStatus, string ApproveRejectComment, string Token)
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
                    if (!string.IsNullOrEmpty(OnDutyRequestId))
                    {
                        long result = await _service.OnDutyRepository.OnDutyVerification(_dataProtector.Unprotect(OnDutyRequestId), ApproveRejectStatus, ApproveRejectComment, _dataProtector.Unprotect(baseModel.UserId));
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
                        else if (result == -3)
                        {
                            msg = "OnDuty cannot be marked due to Attendance already exist";
                        }
                        else if (result == -2)
                        {
                            msg = "Invalid Status";
                        }
                        else if (result == -1)
                        {
                            msg = "OnDuty request not found";
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

        #region Onduty Request Report

        [HttpGet]
        public async Task<IActionResult> OnDutyrequestReport(string userid, string YomaId, string AttendenceFrom, string AttendenceTo, string status)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                userid = _dataProtector.Unprotect(baseModel.UserId);
                dt = await _service.OnDutyRepository.GetOndutyRequestExport(userid, YomaId, AttendenceFrom, AttendenceTo, status);
                //Do Export for 
                if (dt != null && dt.Rows.Count > 0)
                {
                    var totalrowcount = dt.Rows.Count;
                    var totalcolcount = dt.Columns.Count;
                    //dt.Columns.Remove("DisplayOrder");
                    ds.Tables.Add(dt.Copy());

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheetPFFormat = wb.Worksheets.Add("Onduty Report");

                        sheetPFFormat.FirstRow().FirstCell().InsertTable(dt);
                        sheetPFFormat.Columns().AdjustToContents();
                        sheetPFFormat.FirstRow().Style.Font.Bold = true;

                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            return File(MyMemoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OndutyReport.xlsx");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("OnDutyRequests", "OnDuty").WithInfo("Info !", "No data found!");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("OnDutyRequests", "OnDuty").WithDanger("Error !", "Something went wrong.please try again.");
            }
        }
        #endregion
    }
}