using ClosedXML.Excel;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Data;
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
    public class ClaimController : BaseController
    {
        private readonly IDataProtector _dataProtector;
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoginUserRepository _loginUserRepo;
        public ClaimController(IUnitOfWork unitOfWork, IDataProtectionProvider protectionProvider, ILoginUserRepository loginUserRepo, IOptions<AppSettings> appsettings = null) : base(unitOfWork, protectionProvider, loginUserRepo, appsettings)
        {
            _appSettings = appsettings.Value;
            _dataProtector = protectionProvider.CreateProtector(_appSettings.ProtectorValue);
            _unitOfWork = unitOfWork;
            _loginUserRepo = loginUserRepo;
        }
        [HttpGet]
        public async Task<IActionResult> ClaimRequests([FromQuery] ClaimRequestsCustom SearchRequest, string sortOrder, string sortColumn, string pagesize, int page = 1)
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Logout", "Home", new { area = "" }).WithWarning("Warning !", "Unauthorized Access.");
            }

            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchUserCode))
            {
                ViewBag.SearchUserCode = SearchRequest.SearchUserCode;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchEmail))
            {
                ViewBag.SearchAttendanceFrom = SearchRequest.SearchEmail;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchAssociateName))
            {
                ViewBag.SearchAttendanceTo = SearchRequest.SearchAssociateName;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchMobileNumber))
            {
                ViewBag.SearchStatusType = SearchRequest.SearchMobileNumber;
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

            ClaimRequestsCustom claimRequestsCustom = new ClaimRequestsCustom();
            SearchRequest.SupervisorId = _dataProtector.Unprotect(baseModel.UserId);
            claimRequestsCustom = await _service.ClaimRequestsRepository.GetClaimRequestsListing(page, PageSize, SearchRequest);
            if (claimRequestsCustom != null && claimRequestsCustom.ClaimRequestListing != null)
            {
                claimRequestsCustom.ClaimRequestListing.ToList().ForEach(c =>
                {
                    c.UserId = _dataProtector.Protect(c.UserId);
                });
            }
            return View(claimRequestsCustom);
        }

        [HttpGet]
        public async Task<IActionResult> UserClaimRequest([FromQuery] ClaimRequestsCustom SearchRequest, string sortOrder, string sortColumn, string pagesize, int page = 1)
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Logout", "Home", new { area = "" }).WithWarning("Warning !", "Unauthorized Access.");
            }
            if (string.IsNullOrEmpty(SearchRequest.UserId) || string.IsNullOrWhiteSpace(SearchRequest.UserId))
            {
                return RedirectToAction("ClaimRequests", "Claim").WithDanger("Error !", "Something went wrong.please try again.");
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.searchDOJ))
            {
                ViewBag.searchDOJ = SearchRequest.searchDOJ;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchClaimType))
            {
                ViewBag.SearchClaimType = SearchRequest.SearchClaimType;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchStatus))
            {
                ViewBag.SearchStatus = SearchRequest.SearchStatus;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchMonth))
            {
                ViewBag.SearchMonth = SearchRequest.SearchMonth;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchYear))
            {
                ViewBag.SearchYear = SearchRequest.SearchYear;
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
            
            var userId= SearchRequest.UserId;
            ViewBag.UserId = SearchRequest.UserId;
            ClaimRequestsCustom claimRequestsCustom1 = new ClaimRequestsCustom();
            if (SearchRequest.UserId != null)
            {
                SearchRequest.UserId = _dataProtector.Unprotect(SearchRequest.UserId);

            }
            claimRequestsCustom1 = await _service.ClaimRequestsRepository.GetUserRequestsListing(page, PageSize, SearchRequest);
            if (claimRequestsCustom1 != null && claimRequestsCustom1.ClaimRequestListing != null)
            {
                claimRequestsCustom1.ClaimRequestListing.ToList().ForEach(c =>
                {
                    c.UserId = _dataProtector.Protect(c.UserId);
                });
            }
            claimRequestsCustom1.UserId = userId;
            claimRequestsCustom1.monthModelsListing = new SelectList(DateTimeFormatInfo.InvariantInfo.MonthNames.Where(m => !String.IsNullOrEmpty(m)).Select((monthName, index) => new SelectListItem { Value = (index + 1).ToString(), Text = monthName }), "Value", "Text");
            claimRequestsCustom1.yearModelsListing = new SelectList(Enumerable.Range(DateTime.Today.Year - 10, 20).Select(x => new SelectListItem() { Text = x.ToString(), Value = x.ToString() }), "Value", "Text");
            return View(claimRequestsCustom1);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveRejectClaimrequest(string ClaimRequestId, string ApproveRejectStatus, string ApproveRejectComment, string Token)
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
                    if (!string.IsNullOrEmpty(ClaimRequestId))
                    {
                        long result = await _service.ClaimRequestsRepository.ClaimApproveReject(ClaimRequestId, ApproveRejectStatus, ApproveRejectComment, _dataProtector.Unprotect(baseModel.UserId));
                        if (result == 1)
                        {
                            if (ApproveRejectStatus == "A")
                            {
                                msg = "Approved successfully.";
                            }
                            else if (ApproveRejectStatus == "R")
                            {
                                msg = "Rejected successfully.";
                            }
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


        #region Export User ClaimRrequest Report

        [HttpGet]
        public async Task<IActionResult> ExportUserClaimrequestReport(string Userid, string Month, string Year)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                ds = await _service.ClaimRequestsRepository.ExportUserClaimrequestList(_dataProtector.Unprotect(Userid), Month, Year);
                //Do Export for 
                if (ds != null && ds.Tables.Count > 0)
                {

                    ds.Tables.Add(dt.Copy());

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheetPFFormat = wb.Worksheets.Add("Total Claim");
                        var sheetPFFormat2 = wb.Worksheets.Add("Travel");
                        var sheetPFFormat3 = wb.Worksheets.Add("Hotel");
                        var sheetPFFormat4 = wb.Worksheets.Add("CourierStationary");
                        var sheetPFFormat5 = wb.Worksheets.Add("Mobile Bills");



                        sheetPFFormat.FirstRow().FirstCell().InsertTable(ds.Tables[0]);
                        sheetPFFormat.Columns().AdjustToContents();
                        sheetPFFormat.FirstRow().Style.Font.Bold = true;

                        sheetPFFormat2.FirstRow().FirstCell().InsertTable(ds.Tables[1]);
                        sheetPFFormat2.Columns().AdjustToContents();
                        sheetPFFormat2.FirstRow().Style.Font.Bold = true;

                        sheetPFFormat3.FirstRow().FirstCell().InsertTable(ds.Tables[2]);
                        sheetPFFormat3.Columns().AdjustToContents();
                        sheetPFFormat3.FirstRow().Style.Font.Bold = true;


                        sheetPFFormat4.FirstRow().FirstCell().InsertTable(ds.Tables[3]);
                        sheetPFFormat4.Columns().AdjustToContents();
                        sheetPFFormat4.FirstRow().Style.Font.Bold = true;


                        sheetPFFormat5.FirstRow().FirstCell().InsertTable(ds.Tables[4]);
                        sheetPFFormat5.Columns().AdjustToContents();
                        sheetPFFormat5.FirstRow().Style.Font.Bold = true;



                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            return File(MyMemoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "UserClaimrequest.xlsx");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("UserClaimrequest", "Claim", new { UserId = Userid }).WithInfo("Info !", "No data found!");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("UserClaimrequest", "Claim", new { UserId = Userid }).WithDanger("Error !", "Something went wrong.please try again.");
            }
        }
        #endregion
    }
}
