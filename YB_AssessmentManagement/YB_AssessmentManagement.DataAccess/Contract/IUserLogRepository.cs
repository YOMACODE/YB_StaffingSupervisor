using System;
using System.Collections.Generic;
using System.Text;
using YB_AssessmentManagement.DataAccess.Entities;

namespace YB_AssessmentManagement.DataAccess.Contract
{
    public interface IUserLogRepository
    {
        public long SaveUserActivityLog(UserLogModel userLog);
    }
}
