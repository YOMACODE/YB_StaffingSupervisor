using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using YB_AssessmentManagement.Controllers;
using YB_AssessmentManagement.DataAccess.UnitOfWork;
using YB_AssessmentManagement.LoginRepository.ILoginRepository;

namespace YB_AssessmentManagement.Areas.Supervisor.Controllers
{
    [Area("Assessment")]
    public class ProfileController : BaseController
    {
        private readonly IDataProtector _dataProtector;
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        public ProfileController(IUnitOfWork unitOfWork, IDataProtectionProvider protectionProvider, ILoginUserRepository loginUserRepo, IOptions<AppSettings> appsettings = null) : base(unitOfWork, protectionProvider, loginUserRepo, appsettings)
        {
            _appSettings = appsettings.Value;
            _dataProtector = protectionProvider.CreateProtector(_appSettings.ProtectorValue);
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
			return View();
	}
    }
}
