using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;
using YB_StaffingSupervisor.Controllers;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;
using YB_StaffingSupervisor.DataAccess.UnitOfWork;
using YB_StaffingSupervisor.LoginRepository.ILoginRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Linq;

namespace YB_StaffingSupervisor.Areas.MyTeam.Controllers
{
    [Area("MyTeam")]
    public class UserClaimRequestController : BaseController
    {
        private readonly IDataProtector _dataProtector;
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        public UserClaimRequestController(IUnitOfWork unitOfWork, IDataProtectionProvider protectionProvider, ILoginUserRepository loginUserRepo, IOptions<AppSettings> appsettings = null) : base(unitOfWork, protectionProvider, loginUserRepo, appsettings)
        {
            _appSettings = appsettings.Value;
            _dataProtector = protectionProvider.CreateProtector(_appSettings.ProtectorValue);
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> UserClaimRequest([FromQuery] UserClaimRequetsCustom SearchRequest, string sortOrder, string sortColumn, string pagesize, int page = 1)
        {
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchClaimType))
            {
                ViewBag.SearchClaimType = SearchRequest.SearchClaimType;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchClaimStatus))
            {
                ViewBag.SearchClaimStatus = SearchRequest.SearchClaimStatus;
            }

            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchDate))
            {
                ViewBag.SearchDate = SearchRequest.SearchDate;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchYear))
            {
                ViewBag.SearchYear = SearchRequest.SearchYear;
            }
            if (!string.IsNullOrWhiteSpace(SearchRequest.SearchMonth))
            {
                ViewBag.SearchMonth = SearchRequest.SearchMonth;
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

            UserClaimRequetsCustom userClaimRequetsCustom = new UserClaimRequetsCustom();
            userClaimRequetsCustom = await _service.UserClaimRequestsRepository.GetUserClaimRequestListing(page, PageSize, SearchRequest);
            userClaimRequetsCustom.monthModelsListing = new SelectList(DateTimeFormatInfo.InvariantInfo.MonthNames.Where(m => !String.IsNullOrEmpty(m)).Select((monthName, index) => new SelectListItem { Value = (index + 1).ToString(), Text = monthName }), "Value", "Text");
            userClaimRequetsCustom.yearModelsListing = new SelectList(Enumerable.Range(DateTime.Today.Year - 3, 10).Select(x => new SelectListItem() { Text = x.ToString(), Value = x.ToString() }), "Value", "Text");
            return View(userClaimRequetsCustom);
        }


    }
}
