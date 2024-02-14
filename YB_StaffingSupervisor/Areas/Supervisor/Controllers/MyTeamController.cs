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
    public class MyTeamController : BaseController
    {
        private readonly IDataProtector _dataProtector;
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoginUserRepository _loginUserRepo;
        public MyTeamController(IUnitOfWork unitOfWork, IDataProtectionProvider protectionProvider, ILoginUserRepository loginUserRepo, IOptions<AppSettings> appsettings = null) : base(unitOfWork, protectionProvider, loginUserRepo, appsettings)
        {
            _appSettings = appsettings.Value;
            _dataProtector = protectionProvider.CreateProtector(_appSettings.ProtectorValue);
            _unitOfWork = unitOfWork;
            _loginUserRepo = loginUserRepo;
        }
        [HttpGet]
        public async Task<IActionResult> TeamMembers([FromQuery] TeamMemberCustom SearchRequest, string sortOrder, string sortColumn, string pagesize, int page = 1)
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Logout", "Home", new { area = "" }).WithWarning("Warning !", "Unauthorized Access.");
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

            SearchRequest.SupervisorUserId = _dataProtector.Unprotect(baseModel.UserId);
            TeamMemberCustom teamMemberCustom = new TeamMemberCustom();
            teamMemberCustom = await _service.MyTeamRepository.GetTeamMembersListing(page, PageSize, SearchRequest);
            teamMemberCustom.DesignationModels = await _service.DesignationRepository.DropdownDesignationList();
            return View(teamMemberCustom);
        }

        #region Export TeamMember Report

        [HttpGet]
        public async Task<IActionResult> ExportTeamMemberReport(string SearchUserCode, string SearchFullName, string SearchMobileNumber, string SearchEmailId, string SearchDesignation, string SearchJoiningDate)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                var supervisorUserId = _dataProtector.Unprotect(baseModel.UserId);
                dt = await _service.MyTeamRepository.ExportTeamMemberList(supervisorUserId, SearchUserCode, SearchFullName, SearchMobileNumber, SearchEmailId, SearchDesignation, SearchJoiningDate);
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

    }
}
