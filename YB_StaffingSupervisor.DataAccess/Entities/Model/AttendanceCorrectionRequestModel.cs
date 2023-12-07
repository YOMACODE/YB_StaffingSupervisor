using System;
using System.Collections.Generic;
using System.Text;

namespace YB_StaffingSupervisor.DataAccess.Entities.Model
{
	public class AttendanceCorrectionRequestModel
	{
        public string AttendanceCorrectionRequestId { get; set; }
        public string SupervisorId { get; set; }
        public string UserId { get; set; }
        public string AttendanceDate { get; set; }
        public string RequestedOnDate { get; set; }
        public string UserCode { get; set; }
		public string FullName { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string Designation { get; set; }
        public string JoiningDate { get; set; }
        public string RequestType { get; set; }
		public string CheckInTime { get; set; }
		public string CheckOutTime { get; set; }
        public string ShiftHours { get; set; }
        public string WorkingHours { get; set; }
        public string CheckInStatus { get; set; }
        public string CheckOutStatus { get; set; }
        public string ApproveRejectStatus { get; set; }
        public string ApproveRejectComment { get; set; }
        public string CheckInTimeFrom { get; set; }
        public string CheckInTimeTo { get; set; }
        public string CheckOutTimeFrom { get; set; }
        public string CheckOutTimeTo { get; set; }
        public string Remark { get; set; }
    }
}
