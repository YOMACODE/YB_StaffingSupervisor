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
    public class OnDutyRepository : BaseRepository, IOnDutyRepository
    {
        public OnDutyRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public async Task<OnDutyRequestCustom> GetOnDutyListing(int Page, int PageSize, OnDutyRequestCustom SearchRequest)
        {
            OnDutyRequestCustom onDutyRequestCustom = new OnDutyRequestCustom();
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
                    List<OnDutyRequestModel> onDutyRequestModels = new List<OnDutyRequestModel>();
                    DataSet dataSet = await Task.Run(() => dbconnect.SPExecuteDataset("[WebApplication_SP].[usp_Supervisor_OnDutyRequest_ApproveReject_SelectAll_SelectById]", sqlparameters, "dataSet"));
                    if (dataSet != null && dataSet.Tables.Count > 0)
                    {
                        if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                            {
                                OnDutyRequestModel onDutyRequestModel = new OnDutyRequestModel();
                                onDutyRequestModel.OnDutyRequestId = dataSet.Tables[0].Rows[i]["DailyAttendanceOnDutyRequestId"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["DailyAttendanceOnDutyRequestId"]);
                                onDutyRequestModel.UserCode = dataSet.Tables[0].Rows[i]["UserCode"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["UserCode"]);
                                onDutyRequestModel.FullName = dataSet.Tables[0].Rows[i]["FullName"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["FullName"]);
                                onDutyRequestModel.AttendanceDate = dataSet.Tables[0].Rows[i]["OnDutyDate"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["OnDutyDate"]);
                                onDutyRequestModel.RequestType = dataSet.Tables[0].Rows[i]["OnDutyType"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["OnDutyType"]);
                                onDutyRequestModel.CheckInTime = dataSet.Tables[0].Rows[i]["CheckInTime"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInTime"]);
                                onDutyRequestModel.CheckOutTime = dataSet.Tables[0].Rows[i]["CheckOutTime"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutTime"]);
                                onDutyRequestModel.WorkingHours = dataSet.Tables[0].Rows[i]["CompleteHours"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CompleteHours"]);
                                onDutyRequestModel.ApproveRejectStatus = dataSet.Tables[0].Rows[i]["ApproveRejectStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectStatus"]);
                                onDutyRequestModel.ApproveRejectComment = dataSet.Tables[0].Rows[i]["ApproveRejectComment"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectComment"]);
                                onDutyRequestModel.CheckInStatus = dataSet.Tables[0].Rows[i]["CheckInStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInStatus"]);
                                onDutyRequestModel.CheckOutStatus = dataSet.Tables[0].Rows[i]["CheckOutStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutStatus"]);
                                onDutyRequestModel.ShiftHours = dataSet.Tables[0].Rows[i]["ShiftHours"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ShiftHours"]);
                                onDutyRequestModel.CheckInTimeFrom = dataSet.Tables[0].Rows[i]["CheckInTimeFrom"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInTimeFrom"]);
                                onDutyRequestModel.CheckInTimeTo = dataSet.Tables[0].Rows[i]["CheckInTimeTo"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInTimeTo"]);
                                onDutyRequestModel.CheckOutTimeFrom = dataSet.Tables[0].Rows[i]["CheckOutTimeFrom"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutTimeFrom"]);
                                onDutyRequestModel.CheckOutTimeTo = dataSet.Tables[0].Rows[i]["CheckOutTimeTo"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutTimeTo"]);
                                onDutyRequestModel.UserId = dataSet.Tables[0].Rows[i]["UserId"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["UserId"]);
                                onDutyRequestModel.EmailId = dataSet.Tables[0].Rows[i]["EmailId"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["EmailId"]);
                                onDutyRequestModel.MobileNumber = dataSet.Tables[0].Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["MobileNumber"]);
                                onDutyRequestModel.RequestedOnDate = dataSet.Tables[0].Rows[i]["CreateDate"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CreateDate"]);
                                onDutyRequestModel.Remark = dataSet.Tables[0].Rows[i]["Remark"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["Remark"]);
                                onDutyRequestModels.Add(onDutyRequestModel);
                            }
                        }
                        var pager = new CustomPagination((dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0 && dataSet.Tables[1].Columns.Contains("TotalRecords") == true) ? Convert.ToInt32(dataSet.Tables[1].Rows[0]["TotalRecords"]) : 0, Page, PageSize);
                        onDutyRequestCustom.onDutyListing = onDutyRequestModels;
                        onDutyRequestCustom.CustomPagination = pager;
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return onDutyRequestCustom;
        }

        public async Task<long> OnDutyVerification(string onDutyRequestId, string approveRejectstatus, string approveRejectComment, string approveRejectBy)
        {
            try
            {
                long result = 0;
                using (var dbConnect = connectionFactory.GetDAL)
                {
                    SqlParameter[] sqlparameters =
                    {
                    new SqlParameter("@intbOnDutyRequestId",SqlDbType.BigInt){ Value = onDutyRequestId},
                    new SqlParameter("@chvnApproveRejectStatus", SqlDbType.NVarChar) { Value = approveRejectstatus },
                    new SqlParameter("@chvnApproveRejectComment", SqlDbType.NVarChar) { Value = approveRejectComment },
                    new SqlParameter("@intbApproveRejectBy",SqlDbType.BigInt){ Value = approveRejectBy},
                    new SqlParameter("@chvnOperationType",SqlDbType.VarChar){ Value = "ApproveRejectOnDuty"}
                    };
                    result = await Task.Run(() => dbConnect.SPExecuteScalarReturnValue("[WebApplication_SP].[usp_Supervisor_OnDutyRequest_ApproveReject_SelectAll_SelectById]", sqlparameters));
                }
                return result;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return 0;
            }
        }
        public async Task<DataTable> GetOndutyRequestExport(string userid, string YomaId, string AttendenceFrom, string AttendenceTo, string status)
        {
            using (var dbconnect = connectionFactory.GetDAL)
            {
                SqlParameter[] sqlparameters =
                {
                    new SqlParameter("@chvnSearchUserCode", SqlDbType.NVarChar) { Value =  YomaId},
                    new SqlParameter("@intbSupervisorId", SqlDbType.NVarChar) { Value =  userid},
                    new SqlParameter("@chvnSearchFromDate", SqlDbType.NVarChar) { Value =  AttendenceFrom},
                    new SqlParameter("@chvnSearchToDate", SqlDbType.NVarChar) { Value = AttendenceTo},
                    new SqlParameter("@chvnSearchStatusType", SqlDbType.NVarChar) { Value = status },
                    new SqlParameter("@chvnOperationType", SqlDbType.NVarChar) { Value = "Export" },
                };
                DataTable dataTable = await Task.Run(() => dbconnect.SPExecuteDataTable("[WebApplication_SP].[usp_Supervisor_OnDutyRequest_ApproveReject_SelectAll_SelectById]", sqlparameters, "dt"));

                return dataTable;
            }
        }
    }
}
