using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using YB_StaffingSupervisor.DataAccess.Entities;
using YB_StaffingSupervisor.DataAccess.UnitOfWork;
namespace YB_StaffingSupervisor.PartialComponents
{
    public class LeftMenuViewComponent : ViewComponent
    {
        readonly IUnitOfWork _service;
        private readonly IDataProtector _protector;
        private readonly AppSettings _appSettings;
        public LeftMenuViewComponent(IUnitOfWork service, IDataProtectionProvider protectionProvider, IOptions<AppSettings> appsettings = null)
        {
            _appSettings = appsettings.Value;
            _service = service;
            _protector = protectionProvider.CreateProtector(_appSettings.ProtectorValue);
        }

        public async Task<IViewComponentResult> InvokeAsync(String userId)
        {
            //var model = _service.LeftMenuRepository.GetLeftMenuList(Convert.ToInt64(_protector.Unprotect(userId)));
            LeftMenuModel model = new LeftMenuModel();
            model.MenuList = HttpContext.Session.GetString("MenuList").ToString();
            return await Task.FromResult((IViewComponentResult)View("LeftMenu", model));
        }
    }
}
