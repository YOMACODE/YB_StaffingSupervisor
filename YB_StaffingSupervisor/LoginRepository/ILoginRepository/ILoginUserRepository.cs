using System.Threading.Tasks;
using YB_StaffingSupervisor.Models;

namespace YB_StaffingSupervisor.LoginRepository.ILoginRepository
{
    public interface ILoginUserRepository
    {      
        LoginUser Authenticate(string username, string password);
        public bool ValidateCurrentToken(string token,string actionUrl);
        bool ClearToken(long UserId);
        public Task<long> SendOtpOnEmail(string UniqueCode);
        public Task<long> VerifyEmailOtp(string UniqueCode, string Otp);
        public Task<long> SaveNewPassword(string UniqueCode, string NewPassword);
    }
}
