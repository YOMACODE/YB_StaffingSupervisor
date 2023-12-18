using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YB_StaffingSupervisor.DataAccess.Entities
{
    public class UserModel
    {
        public long? UserId { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }
        public long? ClientId { get; set; }

        [Display(Name = "Role")]
        public string RoleName { get; set; }

        public bool? IsAdmin { get; set; }

        public short? RegionId { get; set; }

        [Display(Name = "Region")]
        public string RegionName { get; set; }

        public short? CountryId { get; set; }
        public string MobileNumber { get; set; }
    }
}
