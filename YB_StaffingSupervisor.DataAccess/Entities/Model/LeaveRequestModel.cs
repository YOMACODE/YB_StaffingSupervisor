using System;
using System.Collections.Generic;
using System.Text;

namespace YB_StaffingSupervisor.DataAccess.Entities.Model
{
	public class LeaveRequestModel
	{
        public string LeaveRequestId { get; set; }
        public string SupervisorId { get; set; }
        public string UserId { get; set; }
        public string UserCode { get; set; }
		public string FullName { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string Designation { get; set; }
        public string JoiningDate { get; set; }
        public string LeaveType { get; set; }
        public string LeaveFromDate { get; set; }
        public string LeaveToDate { get; set; }
        public string LeaveFromHalfFullDay { get; set; }
        public string LeaveToHalfFullDay { get; set; }
        public string LeaveReason { get; set; }
        public string RequestedOnDate { get; set; }
        public string ApproveRejectStatus { get; set; }
        public string ApproveRejectComment { get; set; }
    }
}
