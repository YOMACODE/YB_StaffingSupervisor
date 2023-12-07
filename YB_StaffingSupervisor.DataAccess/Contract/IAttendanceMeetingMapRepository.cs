using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Entities;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
    public interface IAttendanceMeetingMapRepository
    {
        Task<AttendanceMeetingMapCustom> GetAttendanceMeetingMapList(string AttendanceDate, string UserId);
    }
}
