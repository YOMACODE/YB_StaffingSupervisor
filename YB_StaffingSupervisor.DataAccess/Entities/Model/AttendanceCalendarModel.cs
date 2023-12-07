using System;
using System.Collections.Generic;
using System.Text;

namespace YB_StaffingSupervisor.DataAccess.Entities.Model
{
	public class AttendanceCalendarModel
	{
        public string SNo { get; set; }
        public string Date { get; set; }
		public string Day { get; set; }
        public string DateDay { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}