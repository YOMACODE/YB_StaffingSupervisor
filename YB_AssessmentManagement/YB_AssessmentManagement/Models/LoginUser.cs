using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace YB_AssessmentManagement.Models
{
    public class LoginUser
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "** Please enter username.")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "** Please enter password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public String Password { get; set; }
        public string Captcha { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpiresOn { get; set; }
        public bool StayLogin { get; set; } = false;
        [TempData]
        public string Message { get; set; }
        public string ReturnUrl { get; set; }
        public string MenuList { get; set; }
    }
}
