using System;
using System.Collections.Generic;
using System.Text;

namespace YB_StaffingSupervisor.DataAccess.Entities.Model
{
	public class AttendanceCorrectionRequestModel
	{
        public string AttendanceCorrectionRequestId { get; set; }
        public string AttendanceDate { get; set; }
        public string RequestedOnDate { get; set; }
        public string UserCode { get; set; }
		public string FullName { get; set; }
		public string RequestType { get; set; }
		public string CheckIn { get; set; }
		public string CheckOut { get; set; }
		public string WorkingHours { get; set; }
		public string Remark { get; set; }
		public string ApproveRejectStatus { get; set; }
		public string ApproveRejectComment { get; set; }
        public string ApproveRejectBy { get; set; }
	}
}
