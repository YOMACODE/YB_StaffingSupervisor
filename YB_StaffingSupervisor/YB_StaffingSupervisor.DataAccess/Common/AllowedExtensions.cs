using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace YB_StaffingSupervisor.DataAccess.Common
{
    public class AllowedExtensions : ValidationAttribute
    {
        private readonly string[] _Extensions;
        public string extensionname;
        public AllowedExtensions(string[] Extensions)
        {
            _Extensions = Extensions;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!(file == null))
                {
                    if (!_Extensions.Contains(extension.ToLower()))
                    {
                        extensionname = extension.ToLower();
                        return new ValidationResult(GetErrorMessage());
                    }
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"This " + extensionname + " extension is not allowed!";
        }
    }
}
