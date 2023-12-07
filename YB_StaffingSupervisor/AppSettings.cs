using System;

namespace  YB_StaffingSupervisor
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string myIssuer { get; set; }
        public string myAudience { get; set; }
        public int ExpiryTime { get; set; }
        public string ProtectorValue { get; set; }
    }
}
