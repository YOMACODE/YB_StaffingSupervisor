using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace YB_AssessmentManagement.DataAccess.Entities
{
    public class LoginModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "** Please enter username.")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "** Please enter password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public bool StayLogin { get; set; }

        [TempData]
        public string Message { get; set; }
        public string MenuList { get; set; }
    }
}
