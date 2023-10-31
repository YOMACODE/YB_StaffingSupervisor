using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Entities;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
    public interface IUserProfileRepository
    {
        UserProfileModel UserProfile(string userId);
    }
}