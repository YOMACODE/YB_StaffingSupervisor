using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Common;
using YB_StaffingSupervisor.DataAccess.Contract;
using YB_StaffingSupervisor.DataAccess.Infrastructure;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;
using YB_StaffingSupervisor.DataAccess.Entities.Model;

namespace YB_StaffingSupervisor.DataAccess.Repository
{
    public class AttendanceRepository : BaseRepository, IAttendanceRepository
    {
        public AttendanceRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public async Task<TeamAttendanceCustom> GetDailyAttendanceListing(TeamAttendanceCustom SearchRequest)
        {
            TeamAttendanceCustom teamAttendanceCustom = new TeamAttendanceCustom();
            using (var dbconnect = connectionFactory.GetDAL)
            {
                SqlParameter[] sqlparameters =
                {
                    new SqlParameter("@intbSupervisorId",SqlDbType.BigInt){Value = SearchRequest.SearchUserCode},
                    new SqlParameter("@chvnSearchDate", SqlDbType.VarChar) { Value = SearchRequest.SearchDate},
                    new SqlParameter("@chvnSearchEmailId", SqlDbType.NVarChar,512) { Value = SearchRequest.SearchEmailId},
                    new SqlParameter("@chvnSearchMobileNumber", SqlDbType.NVarChar,512) { Value = SearchRequest.SearchMobileNumber },
                    new SqlParameter("@chvnSearchUserCode", SqlDbType.NVarChar,512) { Value = SearchRequest.SearchUserCode},
                    new SqlParameter("@chvnOperationType", SqlDbType.NVarChar,512) { Value = "SelectAll" },
                };
                List<AttendanceModel> quickAttendanceModels = new List<AttendanceModel>();
                List<AttendanceModel> selfieBasedAttendanceModels = new List<AttendanceModel>();
                DataSet dataSet = await Task.Run(() => dbconnect.SPExecuteDataset("[WebApplication_SP].[usp_Supervisor_AttendanceRequest_ApproveReject_SelectAll_SelectById]", sqlparameters, "dataSet"));
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {
                            AttendanceModel attendanceModel = new AttendanceModel();
                            attendanceModel.DailyAttendanceId = dataSet.Tables[0].Rows[i]["DailyAttendanceId"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["DailyAttendanceId"]);
                            attendanceModel.AttendanceType = dataSet.Tables[0].Rows[i]["AttendanceType"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["AttendanceType"]);
                            //attendanceModel.SNo = dataSet.Tables[0].Rows[i]["SNo"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["SNo"]);
                            quickAttendanceModels.Add(attendanceModel);
                        }
                        teamAttendanceCustom.quickAttendanceListing = quickAttendanceModels;
                    }
                    if (dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                        {
                            AttendanceModel attendanceModel = new AttendanceModel();
                            attendanceModel.DailyAttendanceId = dataSet.Tables[1].Rows[i]["DailyAttendanceId"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[1].Rows[i]["DailyAttendanceId"]);
                            attendanceModel.AttendanceType = dataSet.Tables[1].Rows[i]["AttendanceType"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["AttendanceType"]);
                            //attendanceModel.SNo = dataSet.Tables[1].Rows[i]["SNo"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["SNo"]);
                            selfieBasedAttendanceModels.Add(attendanceModel);
                        }
                        teamAttendanceCustom.selfieBasedAttendanceListing = selfieBasedAttendanceModels;
                    }
                    if (dataSet.Tables[2] != null && dataSet.Tables[2].Rows.Count > 0)
                    {
                        teamAttendanceCustom.TotalAssociates = dataSet.Tables[2].Rows[0]["TotalAssociates"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[0]["TotalAssociates"]);
                        teamAttendanceCustom.TotalPresent = dataSet.Tables[2].Rows[0]["TotalPresent"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[0]["TotalPresent"]);
                        teamAttendanceCustom.TotalAbsent = dataSet.Tables[2].Rows[0]["TotalAbsent"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[0]["TotalAbsent"]);
                    }
                }
            }
            return teamAttendanceCustom;
        }
        public async Task<UserAttendanceCustom> GetUserAttendanceListing(UserAttendanceCustom SearchRequest)
        {
            UserAttendanceCustom userAttendanceCustom = new UserAttendanceCustom();
            using (var dbconnect = connectionFactory.GetDAL)
            {
                SqlParameter[] sqlparameters =
                {
                    new SqlParameter("@intbSupervisorId",SqlDbType.BigInt){Value = SearchRequest.SearchSupervisorId},
                    new SqlParameter("@chvnSearchMonth", SqlDbType.NVarChar,512) { Value = SearchRequest.SearchMonth},
                    new SqlParameter("@chvnSearchYear", SqlDbType.NVarChar,512) { Value = SearchRequest.SearchYear },
                    new SqlParameter("@chvnSearchUserCode", SqlDbType.NVarChar,512) { Value = SearchRequest.SearchUserCode},
                    new SqlParameter("@chvnOperationType", SqlDbType.NVarChar,512) { Value = "SelectAll" },
                };
                List<AttendanceModel> attendanceModels = new List<AttendanceModel>();
                DataSet dataSet = await Task.Run(() => dbconnect.SPExecuteDataset("[WebApplication_SP].[usp_Supervisor_AttendanceRequest_ApproveReject_SelectAll_SelectById]", sqlparameters, "dataSet"));
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {
                            AttendanceModel attendanceModel = new AttendanceModel();
                            attendanceModel.DailyAttendanceId = dataSet.Tables[0].Rows[i]["DailyAttendanceId"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["DailyAttendanceId"]);
                            attendanceModel.AttendanceType = dataSet.Tables[0].Rows[i]["AttendanceType"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["AttendanceType"]);
                            //attendanceModel.SNo = dataSet.Tables[0].Rows[i]["SNo"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["SNo"]);
                            attendanceModels.Add(attendanceModel);
                        }
                        userAttendanceCustom.userAttendanceListing = attendanceModels;
                    }
                    if (dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0)
                    {
                        userAttendanceCustom.TotalWorkingDays = dataSet.Tables[1].Rows[0]["TotalWorkingDays"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[0]["TotalWorkingDays"]);
                        userAttendanceCustom.TotalPresent = dataSet.Tables[1].Rows[0]["TotalPresent"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[0]["TotalPresent"]);
                        userAttendanceCustom.TotalAbsent = dataSet.Tables[1].Rows[0]["TotalAbsent"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[0]["TotalAbsent"]);
                        userAttendanceCustom.TotalWorkingHours = dataSet.Tables[1].Rows[0]["TotalWorkingHours"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[0]["TotalWorkingHours"]);
                    }
                }
            }
            return userAttendanceCustom;
        }

        public async Task<long> AttendanceVerification(string attendanceId, int approveRejectstatus, string approveRejectComment, string approveRejectBy)
        {
            try
            {
                long result = 0;
                using (var dbConnect = connectionFactory.GetDAL)
                {
                    SqlParameter[] sqlparameters =
                    {
                    new SqlParameter("@intbDailyAttendanceId",SqlDbType.BigInt){ Value = attendanceId},
                    new SqlParameter("@chvnApproveRejectStatus", SqlDbType.NVarChar) { Value = approveRejectstatus },
                    new SqlParameter("@chvnApproveRejectComment", SqlDbType.NVarChar) { Value = approveRejectComment },
                    new SqlParameter("@intbApproveRejectBy",SqlDbType.BigInt){ Value = approveRejectBy},
                    new SqlParameter("@chvnOperationType",SqlDbType.NVarChar){ Value = "ApproveRejectAttendance"}
                    };
                    result = await Task.Run(() => dbConnect.SPExecuteScalarReturnValue("[WebApplication_SP].[usp_Supervisor_AttendanceRequest_ApproveReject_SelectAll_SelectById]", sqlparameters));
                }
                return result;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return 0;
            }
        }
    }
}
