using System;
using System.Data;
using System.Data.SqlClient;
using YB_StaffingSupervisor.DataAccess.Contract;
using YB_StaffingSupervisor.DataAccess.Entities;
using YB_StaffingSupervisor.DataAccess.Infrastructure;

namespace YB_StaffingSupervisor.DataAccess.Repository
{
    public class ErrorLogRepository : BaseRepository, IErrorLogRepository
    {
        public ErrorLogRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        #region Save Error Log
        public long SaveErrorLog(ErrorLogModel  errorLogModel)
        {
            try
            {
                using var dbconnect = connectionFactory.GetErrorLogDAL;
                SqlParameter[] sqlParameters =
                {
                        new SqlParameter("@chvnServerType", SqlDbType.VarChar) { Value = errorLogModel.ApplicationType },
                        new SqlParameter("@chvnExceptionMessage", SqlDbType.NVarChar,-1) { Value = errorLogModel.ExceptionMessage },
                        new SqlParameter("@chvnArea", SqlDbType.VarChar, 512) { Value = errorLogModel.Area },
                        new SqlParameter("@chvnControllerName", SqlDbType.VarChar,512) { Value =errorLogModel.Controller },
                        new SqlParameter("@chvnActionName", SqlDbType.VarChar, 512) { Value = errorLogModel.Action },
                        new SqlParameter("@chvnExceptionStackTrack", SqlDbType.NVarChar, -1) { Value = errorLogModel.ExceptionStackTrack },
                        new SqlParameter("@intbUserId", SqlDbType.BigInt) { Value = Convert.ToInt64(errorLogModel.UserId) },
                };
                long result = Convert.ToInt64(dbconnect.SPExecuteScalar("[ProantoV2Management].[SP_WebExceptionLogger_Insert]", sqlParameters));
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
