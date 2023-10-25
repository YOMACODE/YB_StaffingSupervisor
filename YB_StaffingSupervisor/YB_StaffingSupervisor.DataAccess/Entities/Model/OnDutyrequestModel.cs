using System;
using System.Collections.Generic;
using System.Text;

namespace YB_StaffingSupervisor.DataAccess.Entities.Model
{
    public class OnDutyrequestModel
    {
        public string Date { get; set; }
        public string Clientid { get; set; }
        public string DailyAttendanceOnDutyRequestId { get; set; }
        public string YomaId { get; set; }
        public string Username { get; set; }
        public string RequestType { get; set; }
        public string RequestedOn { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public string ApprovedBy { get; set; }
        public string Comment { get; set; }
    }
}
