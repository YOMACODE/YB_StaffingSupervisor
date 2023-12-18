using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YB_StaffingSupervisor.DataAccess.Entities
{
    public class UserProfileModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string ProfileImage { get; set; }
    }
}
