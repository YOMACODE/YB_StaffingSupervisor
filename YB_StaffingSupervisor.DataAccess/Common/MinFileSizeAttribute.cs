using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace YB_StaffingSupervisor.DataAccess.Common
{
    public class MinFileSizeAttribute : ValidationAttribute
    {
        private readonly int _minFileSize;
        public MinFileSizeAttribute(int minFileSize)
        {
            _minFileSize = minFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length < _minFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"* Minimum allowed file size is { _minFileSize} bytes.";
        }
    }
}
