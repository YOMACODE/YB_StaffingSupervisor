using System;
using System.Collections.Generic;
using System.Text;

namespace YB_AssessmentManagement.DataAccess.Entities
{
    public class UserSettingModel
    {
        public string UserId { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string WebThemeCode { get; set; }
        public string IsCollapseSidebarEnabled { get; set; }
        public string IsFixedSidebarEnabled { get; set; }
        public string IsFixedHeaderEnabled { get; set; }
        public string IsBoxedLayoutEnabled { get; set; }
    }
}
