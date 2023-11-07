using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using YB_StaffingSupervisor.DataAccess.Common;
using YB_StaffingSupervisor.DataAccess.Entities.Model;

namespace YB_StaffingSupervisor.DataAccess.Entities.Custom
{
	public class TeamAttendanceCustom
	{
		#region Search and Sorting Parameter
		public string SearchUserCode { get; set; }
		public string SearchFullName { get; set; }
		public string SearchMobileNumber { get; set; }
		public string SearchEmailId { get; set; }
		public string SearchDate { get; set; }
        public string SupervisorId { get; set; }
        #endregion
        
		#region Listings
        public List<AttendanceModel> selfieBasedAttendanceListing { get; set; }
        public List<AttendanceModel> quickAttendanceListing { get; set; }
        #endregion
		
        #region Other View Parameter
        public string TotalAssociates { get; set; }
        public string TotalPresent { get; set; }
        public string TotalAbsent { get; set; }
        public string Date { get; set; }
        #endregion
    }
}
