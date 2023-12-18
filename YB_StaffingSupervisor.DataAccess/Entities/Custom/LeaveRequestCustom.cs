using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using YB_StaffingSupervisor.DataAccess.Common;
using YB_StaffingSupervisor.DataAccess.Entities.Model;

namespace YB_StaffingSupervisor.DataAccess.Entities.Custom
{
	public class LeaveRequestCustom
	{
        #region Search and Sorting Parameter
        public string SupervisorId { get; set; }
        public string SearchUserCode { get; set; }
        public string SearchLeaveFrom { get; set; }
        public string SearchLeaveTo { get; set; }
        public string SearchStatusType { get; set; }
        public string SortOrderBy { get; set; }
		public string SortColumnName { get; set; }
		public int? PageSize { get; set; }
        #endregion
        
		#region Listings
        public List<LeaveRequestModel> leaveListing { get; set; }
        #endregion
		
		#region Pagination
		public CustomPagination CustomPagination { get; set; }
		#endregion

	}
}
