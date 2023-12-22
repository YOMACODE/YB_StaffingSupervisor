using YB_StaffingSupervisor.DataAccess.Entities;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
    public interface IErrorLogRepository
    {
        public long SaveErrorLog(ErrorLogModel errorLogModel);
    }
}
