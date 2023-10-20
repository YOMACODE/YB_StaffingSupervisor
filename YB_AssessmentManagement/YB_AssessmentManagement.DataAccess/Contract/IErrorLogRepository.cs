using YB_AssessmentManagement.DataAccess.Entities;

namespace YB_AssessmentManagement.DataAccess.Contract
{
    public interface IErrorLogRepository
    {
        public long SaveErrorLog(ErrorLogModel errorLogModel);
    }
}
