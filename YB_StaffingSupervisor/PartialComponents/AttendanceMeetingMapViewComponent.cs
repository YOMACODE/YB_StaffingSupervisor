using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using YB_StaffingSupervisor.DataAccess.UnitOfWork;
using YB_StaffingSupervisor.Models;
using YB_StaffingSupervisor.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;

namespace YB_StaffingSupervisor.PartialComponents
{
    public class AttendanceMeetingMapViewComponent : ViewComponent
    {
        readonly IUnitOfWork _unitOfWork;
        private readonly IDataProtector _protector;
        private readonly AppSettings _appSettings;


        public AttendanceMeetingMapViewComponent(IUnitOfWork service, IDataProtectionProvider protectionProvider, IOptions<AppSettings> appsettings = null)
        {
            _appSettings = appsettings.Value;
            _unitOfWork = service;
            _protector = protectionProvider.CreateProtector(_appSettings.ProtectorValue);
        }
        public async Task<IViewComponentResult> InvokeAsync(string attendanceDate, string userId)
        {
            if (!string.IsNullOrEmpty(attendanceDate) && !string.IsNullOrEmpty(userId))
            {
                AttendanceMeetingMapCustom attendanceMeetingMapCustom = new AttendanceMeetingMapCustom();

                attendanceMeetingMapCustom = await _unitOfWork.AttendanceMeetingMapRepository.GetAttendanceMeetingMapList(attendanceDate ,_protector.Unprotect(userId));
                attendanceMeetingMapCustom.AttendanceDate = attendanceDate;

                return await Task.Run(() => (IViewComponentResult)View("AttendanceMeetingMap", attendanceMeetingMapCustom)).ConfigureAwait(false);
            }
            else
            {
                return await Task.Run(() => (IViewComponentResult)View("")).ConfigureAwait(false);
            }
        }
    }
}
