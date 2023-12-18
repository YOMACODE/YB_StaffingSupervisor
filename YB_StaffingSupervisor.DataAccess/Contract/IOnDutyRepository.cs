using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Entities;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
    public interface IOnDutyRepository
    {
        Task<OnDutyRequestCustom> GetOnDutyListing(int Page, int PageSize, OnDutyRequestCustom SearchRequest);
        Task<long> OnDutyVerification(string onDutyRequestId, string approveRejectstatus, string approveRejectComment, string approveRejectBy);
        Task<DataTable> GetOndutyRequestExport(string userid, string YomaId, string AttendenceFrom, string AttendenceTo, string status);
    }
}