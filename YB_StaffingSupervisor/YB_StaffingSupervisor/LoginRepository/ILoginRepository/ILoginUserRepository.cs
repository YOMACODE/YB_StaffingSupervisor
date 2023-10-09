using YB_StaffingSupervisor.Models;

namespace YB_StaffingSupervisor.LoginRepository.ILoginRepository
{
    public interface ILoginUserRepository
    {      
        LoginUser Authenticate(string username, string password);
        public bool ValidateCurrentToken(string token,string actionUrl);
        bool ClearToken(long UserId);
    }
}
