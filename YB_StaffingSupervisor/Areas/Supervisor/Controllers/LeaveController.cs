using ClosedXML.Excel;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Data;
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
    public class LeaveController : BaseController
    {
        private readonly IDataProtector _dataProtector;
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoginUserRepository _loginUserRepo;
        public LeaveController(IUnitOfWork unitOfWork, IDataProtectionProvider protectionProvider, ILoginUserRepository loginUserRepo, IOptions<AppSettings> appsettings = null) : base(unitOfWork, protectionProvider, loginUserRepo, appsettings)
        {
            _appSettings = appsettings.Value;
            _dataProtector = protectionProvider.CreateProtector(_appSettings.ProtectorValue);
            _unitOfWork = unitOfWork;
            _loginUserRepo = loginUserRepo;
        }
        [HttpGet]
        public async Task<IActionResult> LeaveRequests([FromQuery] LeaveRequestCustom SearchRequest, string sortOrder, string sortColumn, string pagesize, int page = 1)
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Logout", "Home", new { area = "" }).WithWarning("Warning !", "Unauthorized Access.");
            }

            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchUserCode))
            {
                ViewBag.SearchUserCode = SearchRequest.SearchUserCode;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchLeaveFrom))
            {
                ViewBag.SearchLeaveFrom = SearchRequest.SearchLeaveFrom;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchLeaveTo))
            {
                ViewBag.SearchLeaveTo = SearchRequest.SearchLeaveTo;
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

            LeaveRequestCustom leaveRequestCustom = new LeaveRequestCustom();
            SearchRequest.SupervisorId = _dataProtector.Unprotect(baseModel.UserId);
            leaveRequestCustom = await _service.LeaveRepository.GetLeaveRequestListing(page, PageSize, SearchRequest);
            if (leaveRequestCustom != null && leaveRequestCustom.leaveListing != null)
            {
                leaveRequestCustom.leaveListing.ToList().ForEach(c =>
                {
                    c.LeaveRequestId = _dataProtector.Protect(c.LeaveRequestId);
                });
            }
            return View(leaveRequestCustom);
        }
        [HttpPost]
        public async Task<IActionResult> ApproveRejectLeave(string LeaveRequestId, string ApproveRejectStatus, string ApproveRejectComment, string Token)
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
                    if (!string.IsNullOrEmpty(LeaveRequestId))
                    {
                        long result = await _service.LeaveRepository.LeaveVerification(_dataProtector.Unprotect(LeaveRequestId), ApproveRejectStatus, ApproveRejectComment, _dataProtector.Unprotect(baseModel.UserId));
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
                            msg = "Attendance not found";
                        }
                        else if (result == -2)
                        {
                            msg = "Invalid Status";
                        }
                        else if (result == -1)
                        {
                            msg = "Leave request not found";
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
        #region Leave Request Report

        [HttpGet]
        public async Task<IActionResult> LeaveRequestReport(string SearchUserCode, string SearchLeaveFrom, string SearchLeaveTo, string SearchStatusType)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                dt = await _service.LeaveRepository.GetLeaveRequestExport(_dataProtector.Unprotect(baseModel.UserId), SearchUserCode, SearchLeaveFrom, SearchLeaveTo, SearchStatusType);
                //Do Export for 
                if (dt != null && dt.Rows.Count > 0)
                {
                    var totalrowcount = dt.Rows.Count;
                    var totalcolcount = dt.Columns.Count;
                    //dt.Columns.Remove("DisplayOrder");
                    ds.Tables.Add(dt.Copy());

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheetPFFormat = wb.Worksheets.Add("Leave Request Report");

                        sheetPFFormat.FirstRow().FirstCell().InsertTable(dt);
                        sheetPFFormat.Columns().AdjustToContents();
                        sheetPFFormat.FirstRow().Style.Font.Bold = true;

                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            return File(MyMemoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LeaveRequestReport.xlsx");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("LeaveRequests", "Leave").WithInfo("Info !", "No data found!");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("LeaveRequests", "Leave").WithDanger("Error !", "Something went wrong.please try again.");
            }
        }
        #endregion
    }
}
