using System;
using System.Data;
using System.Data.SqlClient;
using YB_AssessmentManagement.DataAccess.Contract;
using YB_AssessmentManagement.DataAccess.Entities;
using YB_AssessmentManagement.DataAccess.Infrastructure;

namespace YB_AssessmentManagement.DataAccess.Repository
{
    public class UserLogRepository : BaseRepository, IUserLogRepository
    {
        public UserLogRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        #region Save User Log
        public long SaveUserActivityLog(UserLogModel userLog)
        {
            try
            {
                using var dbconnect = connectionFactory.GetUserLogDAL;
                SqlParameter[] sqlParameters =
                {
                  new SqlParameter("@chvUserId", SqlDbType.BigInt) { Value = userLog.UserId },
                  new SqlParameter("@chvUrl", SqlDbType.NVarChar, 512) { Value = userLog.Url },
                  new SqlParameter("@chvArea", SqlDbType.VarChar, 100) { Value = userLog.Area },
                  new SqlParameter("@chvController", SqlDbType.VarChar,100) { Value =userLog.Controller },
                  new SqlParameter("@chvAction", SqlDbType.VarChar, 100) { Value = userLog.Action },
                  new SqlParameter("@chvnParameter", SqlDbType.NVarChar, -1) { Value = userLog.Parameter },
                  new SqlParameter("@chvIpAddress", SqlDbType.VarChar, 100) { Value = userLog.IpAddress },
                  new SqlParameter("@chvnToken", SqlDbType.NVarChar,-1) { Value = userLog.Token },
                  new SqlParameter("@chvBrowser", SqlDbType.VarChar, 100) { Value = userLog.Browser },
                  new SqlParameter("@chvOS", SqlDbType.VarChar, 100) { Value = userLog.OS },
                  new SqlParameter("@chvDevice", SqlDbType.VarChar, 100) { Value = userLog.Device }
                };
                long result = Convert.ToInt64(dbconnect.SPExecuteScalar("usp_Save_UserLogInformation", sqlParameters));
                return result;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return 0;
            }
        }
        #endregion
    }
}
