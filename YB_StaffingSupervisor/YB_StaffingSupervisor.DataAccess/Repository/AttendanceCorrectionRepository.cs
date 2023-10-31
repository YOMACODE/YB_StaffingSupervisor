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
    public class AttendanceCorrectionRepository : BaseRepository, IAttendanceCorrectionRepository
    {
        public AttendanceCorrectionRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public async Task<AttendanceCorrectionRequestCustom> GetAttendanceCorrectionListing(int Page, int PageSize, AttendanceCorrectionRequestCustom SearchRequest)
        {
            AttendanceCorrectionRequestCustom attendanceCorrectionRequestCustom = new AttendanceCorrectionRequestCustom();
            try
            {
                using (var dbconnect = connectionFactory.GetDAL)
                {
                    SqlParameter[] sqlparameters =
                    {
                    new SqlParameter("@intbSupervisorId",SqlDbType.BigInt){Value = SearchRequest.SupervisorId},
                    new SqlParameter("@chvnSearchAttendanceDate", SqlDbType.NVarChar,512) { Value = SearchRequest.SearchAttendanceDate},
                    new SqlParameter("@intOffsetValue",SqlDbType.Int){ Value=(Page-1) * PageSize },
                    new SqlParameter("@intPagingSize",SqlDbType.Int){ Value=PageSize },
                    new SqlParameter("@chvnSortOrderBy", SqlDbType.NVarChar,512) { Value = SearchRequest.SortOrderBy},
                    new SqlParameter("@chvnSortColumnName", SqlDbType.NVarChar,512) { Value = SearchRequest.SortColumnName},
                    new SqlParameter("@chvnOperationType", SqlDbType.NVarChar,512) { Value = "SelectAll" },
                };
                    List<AttendanceCorrectionRequestModel> attendanceCorrectionRequestModels = new List<AttendanceCorrectionRequestModel>();
                    DataSet dataSet = await Task.Run(() => dbconnect.SPExecuteDataset("[WebApplication_SP].[usp_Supervisor_AttendanceCorrectionRequest_ApproveReject_SelectAll_SelectById]", sqlparameters, "dataSet"));
                    if (dataSet != null && dataSet.Tables.Count > 0)
                    {
                        if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                            {
                                AttendanceCorrectionRequestModel attendanceCorrectionRequestModel = new AttendanceCorrectionRequestModel();
                                attendanceCorrectionRequestModel.AttendanceCorrectionRequestId = dataSet.Tables[0].Rows[i]["DailyAttendanceId"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["DailyAttendanceId"]);
                                attendanceCorrectionRequestModel.RequestType = dataSet.Tables[0].Rows[i]["AttendanceType"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["AttendanceType"]);
                                //attendanceModel.SNo = dataSet.Tables[0].Rows[i]["SNo"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["SNo"]);
                                attendanceCorrectionRequestModels.Add(attendanceCorrectionRequestModel);
                            }
                        }
                        var pager = new CustomPagination((dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0 && dataSet.Tables[1].Columns.Contains("TotalRecords") == true) ? Convert.ToInt32(dataSet.Tables[1].Rows[0]["TotalRecords"]) : 0, Page, PageSize);
                        attendanceCorrectionRequestCustom.attendanceCorrectionListing = attendanceCorrectionRequestModels;
                        attendanceCorrectionRequestCustom.CustomPagination = pager;
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return attendanceCorrectionRequestCustom;
        }

        public async Task<long> AttendanceCorrectionVerification(string attendanceCorrectionRequestId, int approveRejectstatus, string approveRejectComment, string approveRejectBy)
        {
            try
            {
                long result = 0;
                using (var dbConnect = connectionFactory.GetDAL)
                {
                    SqlParameter[] sqlparameters =
                    {
                    new SqlParameter("@intbDailyAttendanceCorrectionRequestId",SqlDbType.BigInt){ Value = Convert.ToInt32(attendanceCorrectionRequestId)},
                    new SqlParameter("@chvnApproveRejectStatus", SqlDbType.NVarChar) { Value = approveRejectstatus },
                    new SqlParameter("@chvnApproveRejectComment", SqlDbType.NVarChar) { Value = approveRejectComment },
                    new SqlParameter("@intbApproveRejectBy",SqlDbType.BigInt){ Value = Convert.ToInt64(approveRejectBy)},
                    new SqlParameter("@chvnOperationType",SqlDbType.NVarChar){ Value = "ApproveRejectAttendanceCorrection"}
                    };
                    result = await Task.Run(() => dbConnect.SPExecuteScalarReturnValue("[WebApplication_SP].[usp_Supervisor_AttendanceCorrectionRequest_ApproveReject_SelectAll_SelectById]", sqlparameters));
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
