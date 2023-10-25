using System;
using System.Collections.Generic;
using System.Text;
using YB_StaffingSupervisor.DataAccess.Common;
using YB_StaffingSupervisor.DataAccess.Entities.Model;

namespace YB_StaffingSupervisor.DataAccess.Entities.Custom
{
    public class OnDutyRequestCustom
    {
        public string SearchUserCode { get; set; }
        public IEnumerable<OnDutyrequestModel> OnDutyList { get; set; }
        public CustomPagination CustomPagination { get; set; }
        public string DailyAttendanceOnDutyRequestId { get; set; }

        public string Status { get; set; }
        public string Userid { get; set; }
        public string Comment { get; set; }
    }
}
