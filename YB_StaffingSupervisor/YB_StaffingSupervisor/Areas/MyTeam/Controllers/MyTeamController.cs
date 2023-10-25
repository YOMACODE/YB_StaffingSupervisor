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

namespace YB_StaffingSupervisor.Areas.MyTeam.Controllers
{
    [Area("MyTeam")]
    public class MyTeamController : BaseController
    {
        private readonly IDataProtector _dataProtector;
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        public MyTeamController(IUnitOfWork unitOfWork, IDataProtectionProvider protectionProvider, ILoginUserRepository loginUserRepo, IOptions<AppSettings> appsettings = null) : base(unitOfWork, protectionProvider, loginUserRepo, appsettings)
        {
            _appSettings = appsettings.Value;
            _dataProtector = protectionProvider.CreateProtector(_appSettings.ProtectorValue);
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> TeamMembers([FromQuery] TeamMemberCustom SearchRequest, string sortOrder, string sortColumn, string pagesize, int page = 1)
        {
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
			if (!string.IsNullOrWhiteSpace(SearchRequest.SearchDesignation))
			{
				ViewBag.SearchDesignation = SearchRequest.SearchDesignation;
			}
			if (!string.IsNullOrWhiteSpace(SearchRequest.SearchJoiningDate))
			{
				ViewBag.SearchJoiningDate = SearchRequest.SearchJoiningDate;
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

			TeamMemberCustom teamMemberCustom = new TeamMemberCustom();
			teamMemberCustom = await _service.MyTeamRepository.GetTeamMembersListing(page, PageSize, SearchRequest);
			return View(teamMemberCustom);
		}

        [HttpGet]
        public async Task<IActionResult> LeaveApproval([FromQuery] LeaveApprovalsCustom SearchRequest, string sortOrder, string sortColumn, string pagesize, int page = 1)
        {
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchUserCode))
            {
                ViewBag.SearchUserCode = SearchRequest.SearchUserCode;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchAssociateName))
            {
                ViewBag.SearchAssociateName = SearchRequest.SearchAssociateName;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchAppliedDate))
            {
                ViewBag.SearchAppliedDate = SearchRequest.SearchAppliedDate;
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

            LeaveApprovalsCustom leaveApprovalsCustom = new LeaveApprovalsCustom();
            leaveApprovalsCustom = await _service.LeaveApprovalsRepository.GetLeaveApprovalsListing(page, PageSize, SearchRequest);
            return View(leaveApprovalsCustom);
        }


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApproveRejectLeave(LeaveApprovalsCustom model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					string UserId = _dataProtector.Unprotect(baseModel.UserId);

					long result = await _service.LeaveApprovalsRepository.InsertApproveRejectLeave(model.LeaveRequestId,model.Status,model.Comment, UserId);

					if (result == 1)
					{
						return RedirectToAction("LeaveApproval", "MyTeam").WithSuccess("Success !", "Leave Approved/Reject successfully.");
					}
					else
					{
						return RedirectToAction("LeaveApproval", "MyTeam").WithWarning("Warning !", "Something went wrong!");
					}
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", ex.Message);
				}
			}
			return RedirectToAction("ApproveRejectLeave", "MyTeam");
		}

		#region Export TeamMember Report

		[HttpGet]
		public async Task<IActionResult> ExportTeamMemberReport(string SearchUserCode, string SearchFullName, string SearchMobileNumber, string SearchEmailId, string SearchDesignation, string SearchJoiningDate)
		{
			try
			{
				DataTable dt = new DataTable();
				DataSet ds = new DataSet();
				//var clientId = !string.IsNullOrEmpty(ClientId) ? _dataProtector.Unprotect(ClientId) : null;
				dt = await _service.MyTeamRepository.ExportTeamMemberList(SearchUserCode, SearchFullName, SearchMobileNumber, SearchEmailId, SearchDesignation, SearchJoiningDate);
				//Do Export for 
				if (dt != null && dt.Rows.Count > 0)
				{
					var totalrowcount = dt.Rows.Count;
					var totalcolcount = dt.Columns.Count;
					//dt.Columns.Remove("DisplayOrder");
					ds.Tables.Add(dt.Copy());

					using (XLWorkbook wb = new XLWorkbook())
					{
						var sheetPFFormat = wb.Worksheets.Add("TeamMember Report");

						sheetPFFormat.FirstRow().FirstCell().InsertTable(dt);
						sheetPFFormat.Columns().AdjustToContents();
						sheetPFFormat.FirstRow().Style.Font.Bold = true;

						Response.Clear();
						Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
						using (MemoryStream MyMemoryStream = new MemoryStream())
						{
							wb.SaveAs(MyMemoryStream);
							return File(MyMemoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TeamMember.xlsx");
						}
					}
				}
				else
				{
					return RedirectToAction("TeamMembers", "MyTeam").WithInfo("Info !", "No data found!");
				}
			}
			catch (Exception ex)
			{
				return RedirectToAction("TeamMembers", "MyTeam").WithDanger("Error !", "Something went wrong.please try again.");
			}
		}
		#endregion


		#region Export Leave Report

		[HttpGet]
		public async Task<IActionResult> ExportLeaveReport(string SearchUserCode, string SearchAssociateName)
		{
			try
			{
				DataTable dt = new DataTable();
				DataSet ds = new DataSet();
				//var clientId = !string.IsNullOrEmpty(ClientId) ? _dataProtector.Unprotect(ClientId) : null;
				dt = await _service.LeaveApprovalsRepository.ExportLeaveApprovalList(SearchUserCode, SearchAssociateName);
				//Do Export for 
				if (dt != null && dt.Rows.Count > 0)
				{
					var totalrowcount = dt.Rows.Count;
					var totalcolcount = dt.Columns.Count;
					//dt.Columns.Remove("DisplayOrder");
					ds.Tables.Add(dt.Copy());

					using (XLWorkbook wb = new XLWorkbook())
					{
						var sheetPFFormat = wb.Worksheets.Add("Leave Approval Reject Report");

						sheetPFFormat.FirstRow().FirstCell().InsertTable(dt);
						sheetPFFormat.Columns().AdjustToContents();
						sheetPFFormat.FirstRow().Style.Font.Bold = true;

						Response.Clear();
						Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
						using (MemoryStream MyMemoryStream = new MemoryStream())
						{
							wb.SaveAs(MyMemoryStream);
							return File(MyMemoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LeaveApprovalReject.xlsx");
						}
					}
				}
				else
				{
					return RedirectToAction("LeaveApproval", "MyTeam").WithInfo("Info !", "No data found!");
				}
			}
			catch (Exception ex)
			{
				return RedirectToAction("LeaveApproval", "MyTeam").WithDanger("Error !", "Something went wrong.please try again.");
			}
		}
		#endregion
	}
}
