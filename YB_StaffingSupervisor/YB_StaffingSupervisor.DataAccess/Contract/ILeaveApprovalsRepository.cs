using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
    public interface ILeaveApprovalsRepository
    {
        Task<LeaveApprovalsCustom> GetLeaveApprovalsListing(int Page, int PageSize, LeaveApprovalsCustom SearchRequest);
        Task<long> InsertApproveRejectLeave(string LeaveRequestId, string Status, string Comment, string UserId);
		Task<DataTable> ExportLeaveApprovalList(string SearchUserCode, string SearchAssociateName);
	}
}
