using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using YB_StaffingSupervisor.DataAccess.Common;
using YB_StaffingSupervisor.DataAccess.Entities.Model;

namespace YB_StaffingSupervisor.DataAccess.Entities.Custom
{
	public class UserAttendanceCustom
	{
		#region Search and Sorting Parameter
		public string SearchUserCode { get; set; }
        public string SearchYear { get; set; }
        public string SearchMonth { get; set; }
        public string SearchSupervisorId { get; set; }
        #endregion

        #region Listings
        public List<AttendanceModel> userAttendanceListing { get; set; }
        
		public SelectList monthModelsListing { get; set; }
        public SelectList yearModelsListing { get; set; }
        #endregion

        #region Other View Parameter
        public string TotalWorkingDays { get; set; }
        public string TotalPresent { get; set; }
        public string TotalAbsent { get; set; }
        public string TotalWorkingHours { get; set; }
        #endregion
    }
}
