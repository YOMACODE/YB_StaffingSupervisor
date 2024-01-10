using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Entities;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
    public interface ILeaveRepository
    {
        Task<LeaveRequestCustom> GetLeaveRequestListing(int Page, int PageSize, LeaveRequestCustom SearchRequest);
        Task<long> LeaveVerification(string leaveRequestId, string approveRejectstatus, string approveRejectComment, string approveRejectBy);

        Task<DataSet> ExportLeaveRequests(string SearchStatusType, string SupervisorId);

    }
}