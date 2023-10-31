using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using YB_StaffingSupervisor.DataAccess.Common;
using YB_StaffingSupervisor.DataAccess.Entities.Model;

namespace YB_StaffingSupervisor.DataAccess.Entities.Custom
{
	public class AttendanceRequestCustom
	{
        #region Search and Sorting Parameter
        public string SupervisorId { get; set; }
        public string SearchAttendanceDate { get; set; }
		public string SortOrderBy { get; set; }
		public string SortColumnName { get; set; }
		public int? PageSize { get; set; }
        #endregion
        
		#region Listings
        public List<AttendanceModel> attendanceListing { get; set; }
        #endregion
		
		#region Pagination
		public CustomPagination CustomPagination { get; set; }
		#endregion

	}
}
