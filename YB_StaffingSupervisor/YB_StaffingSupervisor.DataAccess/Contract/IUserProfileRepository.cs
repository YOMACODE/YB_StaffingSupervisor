using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Entities;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
    public interface IUserProfileRepository
    {
        Task<UserProfileModel> UserProfile(string userId);
    }
}