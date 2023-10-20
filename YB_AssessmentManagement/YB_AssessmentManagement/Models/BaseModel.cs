using System;

namespace YB_AssessmentManagement.Models
{
    public class BaseModel
    {
        public string UserId { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public bool? IsAdmin { get; set; }
        public string RegionName { get; set; }
        public short? CountryId { get; set; }
        public string SiteURL { get; set; }
        public string TokenValue { get; set; }
        public string WebThemeCode { get; set; }
        public string IsCollapseSidebarEnabled { get; set; }
        public string IsFixedSidebarEnabled { get; set; }
        public string IsFixedHeaderEnabled { get; set; }
        public string IsBoxedLayoutEnabled { get; set; }
        public string MobileNumber { get; set; }

        public string PartialClientId{get;set;}
    }
}
