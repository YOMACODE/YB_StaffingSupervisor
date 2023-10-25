using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;
using YB_StaffingSupervisor.DataAccess.Entities.Model;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
    public interface IOnDutyRequesteRepository
    {
        Task<OnDutyRequestCustom> GetOnDutyRequestListing(int Page, int PageSize);

        Task<long> InsertOnDutyRequest(string DailyAttendanceOnDutyRequestId, string Status, string Comment, string UserId);
    }
}
