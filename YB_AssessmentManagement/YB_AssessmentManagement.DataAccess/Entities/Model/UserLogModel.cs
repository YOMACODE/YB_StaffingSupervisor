using System;
using System.Collections.Generic;
using System.Text;

namespace YB_AssessmentManagement.DataAccess.Entities
{
    public class UserLogModel
    {
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Parameter { get; set; }
        public string Url { get; set; }
        public string UserId { get; set; }
        public string IpAddress { get; set; }
        public string Token { get; set; }
        public string Browser { get; set; }
        public string OS { get; set; }
        public string Device { get; set; }
        public string CreatedDate { get; set; }

    }
}
