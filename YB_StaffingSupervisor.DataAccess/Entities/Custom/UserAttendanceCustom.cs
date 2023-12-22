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
        public string SearchYear { get; set; }
        public string SearchMonth { get; set; }
        public string SupervisorId { get; set; }
        public string UserId { get; set; }
        #endregion

        #region Listings
        public List<AttendanceModel> attendanceListing { get; set; }
        public List<AttendanceCalendarModel> attendanceCalendarListing { get; set; }
        
		public SelectList monthModelsListing { get; set; }
        public SelectList yearModelsListing { get; set; }
        #endregion

        #region Other View Parameter
        public string FullName { get; set; }
        public string UserCode { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string TotalWorkingDays { get; set; }
        public string TotalPresents { get; set; }
        public string TotalAbsents { get; set; }
        public string TotalLeaves { get; set; }
        public string TotalWeekoffs { get; set; }
        public string TotalHolidays { get; set; }
        public string TotalWorkingHours { get; set; }
        #endregion
    }
}
