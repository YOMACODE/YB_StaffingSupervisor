using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
	public interface IClaimRequestsRepository
	{
		Task<ClaimRequestsCustom> GetClaimRequestsListing(int Page, int PageSize, ClaimRequestsCustom SearchRequest);
		Task<ClaimRequestsCustom> GetUserRequestsListing(int Page, int PageSize, ClaimRequestsCustom SearchRequest1);
        Task<DataTable> ExportClaimRequestsList(string SearchUserCode, string SearchAssociateName, string SearchEmail, string SearchMobileNumber);
        Task<DataSet> ExportUserClaimrequestList(string ClaimType, string ClaimStatus, string Month, string Year);

        Task<long> ClaimApproveReject(string ClaimRequestId, string ApproveRejectStatus, string ApproveRejectComment, string Token);
    }
}
