using System;
using System.Collections.Generic;
using System.Text;
using YB_StaffingSupervisor.DataAccess.Common;
using YB_StaffingSupervisor.DataAccess.Entities.Model;

namespace YB_StaffingSupervisor.DataAccess.Entities.Custom
{
	public	class ClaimRequestsCustom
	{
		#region Search and Sorting Parameter
		public string SearchUserCode { get; set; }
		public string claimRequestId { get; set; }
		public string SupervisorId { get; set; }
		public string SearchAssociateName { get; set; }
        public string SearchFrom { get; set; }
        public string SearchTo { get; set; }
        public string SearchEmail { get; set; }
		public string SearchMobileNumber { get; set; }
        public string SearchClaimType { get; set; }
		public string searchDOJ { get; set; }

        //public string SearchAppliedDate { get; set; }
        public string SearchStatus { get; set; }

		public string SortOrderBy { get; set; }
		public string SortColumnName { get; set; }
		public int? PageSize { get; set; }
		public string Comment { get; set; }
		public string Status { get; set; }
		public string UserId { get; set; }

	//	public string LeaveRequestId { get; set; }
		#endregion
		public IEnumerable<ClaimRequestsModel> ClaimRequestsModels { get; set; }
		public IEnumerable<ClaimRequestsModel> ClaimRequestListing { get; set; }
		#region Pagination
		public CustomPagination CustomPagination { get; set; }
		#endregion
	}
}
