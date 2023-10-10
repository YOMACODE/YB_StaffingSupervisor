using System;
using System.Collections.Generic;
using System.Text;
using YB_StaffingSupervisor.DataAccess.Common;
using YB_StaffingSupervisor.DataAccess.Entities.Model;

namespace YB_StaffingSupervisor.DataAccess.Entities.Custom
{
	public class TeamMemberCustom
	{
		#region Search and Sorting Parameter
		public string SearchUserCode { get; set; }
		public string SearchFullName { get; set; }
		public string SearchMobileNumber { get; set; }
		public string SearchEmailId { get; set; }
		public string SearchDesignation { get; set; }
		public string SearchJoiningDate { get; set; }
		public string SortOrderBy { get; set; }
		public string SortColumnName { get; set; }
		public int? PageSize { get; set; }
		#endregion
		public IEnumerable<TeamMemberModel> TeamMemberModels { get; set; }
		public IEnumerable<TeamMemberModel> TeamMemberListing { get; set; }
		#region Pagination
		public CustomPagination CustomPagination { get; set; }
		#endregion

	}
}
