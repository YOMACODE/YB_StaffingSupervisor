using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YB_AssessmentManagement.DataAccess.Entities;

namespace YB_AssessmentManagement.DataAccess.Contract
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
    }
}