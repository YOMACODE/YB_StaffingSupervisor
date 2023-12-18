using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace YB_StaffingSupervisor.DataAccess.Common
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class EmailAddressExAttribute : DataTypeAttribute
    {
        #region privates
        private readonly EmailAddressAttribute _emailAddressAttribute = new EmailAddressAttribute();
        #endregion

        #region ctor
        public EmailAddressExAttribute() : base(DataType.EmailAddress) { }
        #endregion

        #region Overrides
        /// <summary>
        /// Checks if the value is valid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            var emailAddr = Convert.ToString(value);
            if (string.IsNullOrWhiteSpace(emailAddr)) return false;

            //lets test for mulitple email addresses
            var emails = emailAddr.Split(new[] { ';', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            return emails.All(t => _emailAddressAttribute.IsValid(t));
        }
        #endregion
    }
}
