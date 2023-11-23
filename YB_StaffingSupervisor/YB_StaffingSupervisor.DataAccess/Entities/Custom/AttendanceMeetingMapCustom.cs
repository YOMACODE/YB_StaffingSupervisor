using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using YB_StaffingSupervisor.DataAccess.Common;
using YB_StaffingSupervisor.DataAccess.Entities.Model;

namespace YB_StaffingSupervisor.DataAccess.Entities.Custom
{
	public class AttendanceMeetingMapCustom
    {
        public string AttendanceDate { get; set; }
        public string TotalDistanceTravel { get; set; }
        public List<AttendanceMeetingMapModel> attendanceMeetingMapListing { get; set; }
	}
}
