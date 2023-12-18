using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Entities;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
    public interface IAttendanceRepository
    {
        Task<TeamAttendanceCustom> GetDailyTeamAttendanceListing(TeamAttendanceCustom SearchRequest);
        Task<AttendanceRequestCustom> GetAttendanceRequestListing(int Page, int PageSize, AttendanceRequestCustom SearchRequest);
        Task<UserAttendanceCustom> GetUserAttendanceListing(UserAttendanceCustom SearchRequest);
        Task<long> AttendanceVerification(string attendanceId, string approveRejectstatus, string approveRejectComment, string approveRejectBy);
        Task<DataTable> GetAttendanceRequestExport(string userid, string YomaId, string AttendenceFrom, string AttendenceTo, string status);
    }
}