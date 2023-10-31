using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Entities;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
    public interface IAttendanceCorrectionRepository
    {
        Task<AttendanceCorrectionRequestCustom> GetAttendanceCorrectionListing(int Page, int PageSize, AttendanceCorrectionRequestCustom SearchRequest);
        Task<long> AttendanceCorrectionVerification(string attendanceCorrectionRequestId, int approveRejectstatus, string approveRejectComment, string approveRejectBy);
    }
}