using System;
using System.Collections.Generic;
using System.Text;

namespace YB_StaffingSupervisor.DataAccess.Entities.Model
{
    public class LeaveApprovalsModel
    {
        public string UserCode { get; set; }
        public string LeaveRequestId { get; set; }
        public string AssociateName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string LeaveType { get; set; }
        public string LeaveDays { get; set; }
        public string AppliedDate { get; set; }
        public string Status { get; set; }
        public string Approvedby { get; set; }
        public string Comment { get; set; }
    }
}
