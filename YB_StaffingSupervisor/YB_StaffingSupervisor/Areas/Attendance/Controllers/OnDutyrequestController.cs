using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;
using YB_StaffingSupervisor.DataAccess.UnitOfWork;
using YB_StaffingSupervisor.LoginRepository.ILoginRepository;
using YB_StaffingSupervisor.Controllers;
using YB_StaffingSupervisor.Common;
using YB_StaffingSupervisor.DataAccess.Entities.Model;

namespace YB_StaffingSupervisor.Areas.Attendance.Controllers
{
    [Area("Attendance")]
    public class OnDutyrequestController : BaseController
    {
        private readonly IDataProtector _dataProtector;
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        public OnDutyrequestController(IUnitOfWork unitOfWork, IDataProtectionProvider protectionProvider, ILoginUserRepository loginUserRepo, IOptions<AppSettings> appsettings = null) : base(unitOfWork, protectionProvider, loginUserRepo, appsettings)
        {
            _appSettings = appsettings.Value;
            _dataProtector = protectionProvider.CreateProtector(_appSettings.ProtectorValue);
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> OnDutyRequest([FromQuery] TeamMemberCustom SearchRequest, string sortOrder, string sortColumn, string pagesize, int page = 1)
        {

            int PageSize;
            if (pagesize == null)
            {
                PageSize = 10;
            }
            else
            {
                PageSize = Convert.ToInt32(pagesize);
            }
            ViewBag.page = page;
            ViewBag.PageSize = PageSize;

            OnDutyRequestCustom onDutyRequestCustom = new OnDutyRequestCustom();
            onDutyRequestCustom = await _service.OnDutyRequesteRepository.GetOnDutyRequestListing(page, PageSize);
            return View(onDutyRequestCustom);
        }


        [HttpPost]

        public async Task<IActionResult> OnDutyRequest(OnDutyRequestCustom model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string UserId = _dataProtector.Unprotect(baseModel.UserId);

                    long result = await _service.OnDutyRequesteRepository.InsertOnDutyRequest(model.DailyAttendanceOnDutyRequestId,model.Comment,model.Status,UserId);

                    if (result == 1)
                    {
                        return RedirectToAction("OnDutyrequest", "Ondutyrequest").WithSuccess("Success !", "OD Approved/Reject successfully.");
                    }
                    else
                    {
                        return RedirectToAction("OnDutyrequest", "Ondutyrequest").WithWarning("Warning !", "Something went wrong!");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return RedirectToAction("OnDutyrequest", "Ondutyrequest");
        }
    }
}
