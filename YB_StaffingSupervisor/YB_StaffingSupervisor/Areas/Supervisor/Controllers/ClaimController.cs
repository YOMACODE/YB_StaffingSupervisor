using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
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
			if (!string.IsNullOrWhiteSpace(SearchRequest.SearchAssociateName))
			{
				ViewBag.SearchAssociateName = SearchRequest.SearchAssociateName;
			}

			if (!string.IsNullOrWhiteSpace(SearchRequest.SearchStatus))
			{
				ViewBag.SearchStatus = SearchRequest.SearchStatus;
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
			claimRequestsCustom = await _service.ClaimRequestsRepository.GetClaimRequestsListing(page, PageSize, SearchRequest);
			return View(claimRequestsCustom);
		}
    }
}
