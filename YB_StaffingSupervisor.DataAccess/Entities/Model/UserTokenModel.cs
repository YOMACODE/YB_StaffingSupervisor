using System;
using System.Collections.Generic;
using System.Text;

namespace YB_StaffingSupervisor.DataAccess.Entities.Model
{
    public class UserTokenModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CreatedBy { get; set; }
        public string Provider { get; set; }
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int TokenExpiresIn { get; set; }
        public string Scope { get; set; }
    }
}
