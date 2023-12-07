using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Entities;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
    public interface IUserRepository
    {
        LoginModel CheckLogin(String username, String password);
        int SaveLoginToken(String username, string token, DateTime tokenExpireOn);        
        bool ValidateClientUserToken(String username, String token, DateTime tokenExpireOn, string actionUrl);
        bool ClearToken(long UserId);
        UserSettingModel UserSetting(Int64? userId);
        void ChangeUserLayout(Int64? userId, String type);
        void ChangeUserTheme(Int64? userId, String themeName);
        UserModel User(Int64? userId);
        Task<IEnumerable<UserModel>> DropDownForUserList();
        Task<long> SendOtpOnEmail(string UniqueCode);
        Task<long> VerifyEmailOtp(string UniqueCode, string Otp);
        Task<long> SaveNewPassword(string UniqueCode, string NewPassword);
    }
}