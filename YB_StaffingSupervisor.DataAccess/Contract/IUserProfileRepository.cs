using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Entities;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
    public interface IUserProfileRepository
    {
        Task<UserProfileModel> UserProfile(string userId);
        Task<long> ChangePassword(string newPassword,string userId);
        Task<long> ChangeProfileImage(string imgSrcPath, string userId);
    }
}