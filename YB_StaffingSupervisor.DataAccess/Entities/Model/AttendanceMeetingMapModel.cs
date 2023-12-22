using System;
using System.Collections.Generic;
using System.Text;

namespace YB_StaffingSupervisor.DataAccess.Entities.Model
{
    public class AttendanceMeetingMapModel
    {
        public string SNo { get; set; }
        public string MeetingTitle { get; set; }
        public string MeetingDescription { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        public string DistanceTravel { get; set; }
    }
}
