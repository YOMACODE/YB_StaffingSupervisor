using System;
using System.Collections.Generic;
using System.Text;

namespace YB_StaffingSupervisor.DataAccess.Entities.Model
{
	public class AttendanceModel
	{
        public string DailyAttendanceId { get; set; }
        public string UserCode { get; set; }
		public string FullName { get; set; }
		public string EmailId { get; set; }
		public string MobileNumber { get; set; }
		public string Designation { get; set; }
		public string JoiningDate { get; set; }
		public string AttendanceType { get; set; }
        public string AttendanceDate { get; set; }
		public string CheckIn { get; set; }
		public string CheckOut { get; set; }
		public string WorkingHours { get; set; }
		public string Arrival { get; set; }
		public string ApproveRejectStatus { get; set; }
		public string ApproveRejectComment { get; set; }
        public string CheckInImage { get; set; }
        public string CheckOutImage { get; set; }
	}
}
