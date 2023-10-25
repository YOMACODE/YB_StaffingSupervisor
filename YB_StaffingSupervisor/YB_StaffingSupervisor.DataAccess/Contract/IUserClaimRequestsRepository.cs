using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
    public interface  IUserClaimRequestsRepository
    {
        Task<UserClaimRequetsCustom> GetUserClaimRequestListing(int Page, int PageSize, UserClaimRequetsCustom SearchRequest);
    }
}
