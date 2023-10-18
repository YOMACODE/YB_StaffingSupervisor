using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
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
			teamMemberCustom.DesignationModels = await _service.DesignationRepository.DropdownDesignationList();
			return View(teamMemberCustom);
		}
    }
}
