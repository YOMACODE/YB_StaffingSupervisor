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
                    new SqlParameter("@chvnSearchUserCode",SqlDbType.VarChar){Value = SearchRequest.SearchUserCode},
                    new SqlParameter("@chvnSearchFromDate",SqlDbType.VarChar){Value = SearchRequest.SearchAttendanceFrom},
                    new SqlParameter("@chvnSearchToDate",SqlDbType.VarChar){Value = SearchRequest.SearchAttendanceTo},
                    new SqlParameter("@chvnSearchStatusType",SqlDbType.VarChar){Value = SearchRequest.SearchStatusType},
                    new SqlParameter("@intOffsetValue",SqlDbType.Int){ Value=(Page-1) * PageSize },
                    new SqlParameter("@intPagingSize",SqlDbType.Int){ Value=PageSize },
                    new SqlParameter("@chvnSortOrderBy", SqlDbType.NVarChar,10) { Value = SearchRequest.SortOrderBy},
                    new SqlParameter("@chvnSortColumnName", SqlDbType.NVarChar,512) { Value = SearchRequest.SortColumnName},
                    new SqlParameter("@chvnOperationType", SqlDbType.VarChar) { Value = "SelectAll" },
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
                                attendanceCorrectionRequestModel.AttendanceCorrectionRequestId = dataSet.Tables[0].Rows[i]["DailyAttendanceCorrectionRequestId"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["DailyAttendanceCorrectionRequestId"]);
                                attendanceCorrectionRequestModel.UserCode = dataSet.Tables[0].Rows[i]["UserCode"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["UserCode"]);
                                attendanceCorrectionRequestModel.FullName = dataSet.Tables[0].Rows[i]["FullName"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["FullName"]);
                                attendanceCorrectionRequestModel.AttendanceDate = dataSet.Tables[0].Rows[i]["AttendanceCorrectionDate"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["AttendanceCorrectionDate"]);
                                attendanceCorrectionRequestModel.RequestType = dataSet.Tables[0].Rows[i]["AttendanceCorrectionType"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["AttendanceCorrectionType"]);
                                attendanceCorrectionRequestModel.CheckInTime = dataSet.Tables[0].Rows[i]["CheckInTime"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInTime"]);
                                attendanceCorrectionRequestModel.CheckOutTime = dataSet.Tables[0].Rows[i]["CheckOutTime"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutTime"]);
                                attendanceCorrectionRequestModel.WorkingHours = dataSet.Tables[0].Rows[i]["CompleteHours"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CompleteHours"]);
                                attendanceCorrectionRequestModel.ApproveRejectStatus = dataSet.Tables[0].Rows[i]["ApproveRejectStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectStatus"]);
                                attendanceCorrectionRequestModel.ApproveRejectComment = dataSet.Tables[0].Rows[i]["ApproveRejectComment"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectComment"]);
                                attendanceCorrectionRequestModel.CheckInStatus = dataSet.Tables[0].Rows[i]["CheckInStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInStatus"]);
                                attendanceCorrectionRequestModel.CheckOutStatus = dataSet.Tables[0].Rows[i]["CheckOutStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutStatus"]);
                                attendanceCorrectionRequestModel.ShiftHours = dataSet.Tables[0].Rows[i]["ShiftHours"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ShiftHours"]);
                                attendanceCorrectionRequestModel.CheckInTimeFrom = dataSet.Tables[0].Rows[i]["CheckInTimeFrom"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInTimeFrom"]);
                                attendanceCorrectionRequestModel.CheckInTimeTo = dataSet.Tables[0].Rows[i]["CheckInTimeTo"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInTimeTo"]);
                                attendanceCorrectionRequestModel.CheckOutTimeFrom = dataSet.Tables[0].Rows[i]["CheckOutTimeFrom"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutTimeFrom"]);
                                attendanceCorrectionRequestModel.CheckOutTimeTo = dataSet.Tables[0].Rows[i]["CheckOutTimeTo"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutTimeTo"]);
                                attendanceCorrectionRequestModel.UserId = dataSet.Tables[0].Rows[i]["UserId"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["UserId"]);
                                attendanceCorrectionRequestModel.EmailId = dataSet.Tables[0].Rows[i]["EmailId"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["EmailId"]);
                                attendanceCorrectionRequestModel.MobileNumber = dataSet.Tables[0].Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["MobileNumber"]);
                                attendanceCorrectionRequestModel.RequestedOnDate = dataSet.Tables[0].Rows[i]["CreateDate"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CreateDate"]);
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

        public async Task<long> AttendanceCorrectionVerification(string attendanceCorrectionRequestId, string approveRejectstatus, string approveRejectComment, string approveRejectBy)
        {
            try
            {
                long result = 0;
                using (var dbConnect = connectionFactory.GetDAL)
                {
                    SqlParameter[] sqlparameters =
                    {
                    new SqlParameter("@intbAttendanceCorrectionRequestId",SqlDbType.BigInt){ Value = attendanceCorrectionRequestId},
                    new SqlParameter("@chvnApproveRejectStatus", SqlDbType.NVarChar) { Value = approveRejectstatus },
                    new SqlParameter("@chvnApproveRejectComment", SqlDbType.NVarChar) { Value = approveRejectComment },
                    new SqlParameter("@intbApproveRejectBy",SqlDbType.BigInt){ Value = approveRejectBy},
                    new SqlParameter("@chvnOperationType",SqlDbType.VarChar){ Value = "ApproveRejectAttendanceCorrection"}
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
