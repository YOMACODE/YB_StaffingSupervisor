using System;
using System.Collections.Generic;
using System.Text;
using YB_StaffingSupervisor.DataAccess.Entities;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
    public interface IUserLogRepository
    {
        public long SaveUserActivityLog(UserLogModel userLog);
    }
}
