using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Entities;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
    public interface IAttendanceRepository
    {
        Task<UserAttendanceCustom> GetUserAttendanceListing(UserAttendanceCustom SearchRequest);
        Task<long> AttendanceVerification(string attendanceId, int approveRejectstatus, string approveRejectComment, string approveRejectBy);
    }
}