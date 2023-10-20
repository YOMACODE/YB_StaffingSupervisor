using YB_AssessmentManagement.Models;

namespace YB_AssessmentManagement.LoginRepository.ILoginRepository
{
    public interface ILoginUserRepository
    {      
        LoginUser Authenticate(string username, string password);
        public bool ValidateCurrentToken(string token,string actionUrl);
        bool ClearToken(long UserId);
    }
}
