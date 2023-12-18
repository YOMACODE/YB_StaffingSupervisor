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
    public class LeaveRepository : BaseRepository, ILeaveRepository
    {
        public LeaveRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public async Task<LeaveRequestCustom> GetLeaveRequestListing(int Page, int PageSize, LeaveRequestCustom SearchRequest)
        {
            LeaveRequestCustom leaveRequestCustom = new LeaveRequestCustom();
            try
            {
                using (var dbconnect = connectionFactory.GetDAL)
                {
                    SqlParameter[] sqlparameters =
                    {
                    new SqlParameter("@intbSupervisorId",SqlDbType.BigInt){Value = SearchRequest.SupervisorId},
                    new SqlParameter("@chvnSearchUserCode",SqlDbType.VarChar){Value = SearchRequest.SearchUserCode},
                    new SqlParameter("@chvnSearchFromDate",SqlDbType.VarChar){Value = SearchRequest.SearchLeaveFrom},
                    new SqlParameter("@chvnSearchToDate",SqlDbType.VarChar){Value = SearchRequest.SearchLeaveTo},
                    new SqlParameter("@chvnSearchStatusType",SqlDbType.VarChar){Value = SearchRequest.SearchStatusType},
                    new SqlParameter("@intOffsetValue",SqlDbType.Int){ Value=(Page-1) * PageSize },
                    new SqlParameter("@intPagingSize",SqlDbType.Int){ Value=PageSize },
                    new SqlParameter("@chvnSortOrderBy", SqlDbType.NVarChar,10) { Value = SearchRequest.SortOrderBy},
                    new SqlParameter("@chvnSortColumnName", SqlDbType.NVarChar,512) { Value = SearchRequest.SortColumnName},
                    new SqlParameter("@chvnOperationType", SqlDbType.VarChar) { Value = "SelectAll" },
                };
                    List<LeaveRequestModel> leaveRequestModels = new List<LeaveRequestModel>();
                    DataSet dataSet = await Task.Run(() => dbconnect.SPExecuteDataset("[WebApplication_SP].[usp_Supervisor_LeaveRequest_ApproveReject_SelectAll_SelectById]", sqlparameters, "dataSet"));
                    if (dataSet != null && dataSet.Tables.Count > 0)
                    {
                        if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                            {
                                LeaveRequestModel leaveRequestModel = new LeaveRequestModel();
                                leaveRequestModel.LeaveRequestId = dataSet.Tables[0].Rows[i]["LeaveRequestId"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["LeaveRequestId"]);
                                leaveRequestModel.UserId = dataSet.Tables[0].Rows[i]["UserId"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["UserId"]);
                                leaveRequestModel.UserCode = dataSet.Tables[0].Rows[i]["UserCode"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["UserCode"]);
                                leaveRequestModel.FullName = dataSet.Tables[0].Rows[i]["FullName"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["FullName"]);
                                leaveRequestModel.EmailId = dataSet.Tables[0].Rows[i]["EmailId"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["EmailId"]);
                                leaveRequestModel.MobileNumber = dataSet.Tables[0].Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["MobileNumber"]);
                                leaveRequestModel.LeaveType = dataSet.Tables[0].Rows[i]["LeaveType"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["LeaveType"]);
                                leaveRequestModel.LeaveFromDate = dataSet.Tables[0].Rows[i]["LeaveFromDate"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["LeaveFromDate"]);
                                leaveRequestModel.LeaveToDate = dataSet.Tables[0].Rows[i]["LeaveToDate"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["LeaveToDate"]);
                                leaveRequestModel.LeaveFromHalfFullDay = dataSet.Tables[0].Rows[i]["LeaveFromHalfFullDay"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["LeaveFromHalfFullDay"]);
                                leaveRequestModel.LeaveToHalfFullDay = dataSet.Tables[0].Rows[i]["LeaveToHalfFullDay"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["LeaveToHalfFullDay"]);
                                leaveRequestModel.LeaveReason = dataSet.Tables[0].Rows[i]["LeaveReason"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["LeaveReason"]);
                                leaveRequestModel.ApproveRejectStatus = dataSet.Tables[0].Rows[i]["ApproveRejectStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectStatus"]);
                                leaveRequestModel.ApproveRejectComment = dataSet.Tables[0].Rows[i]["ApproveRejectComment"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectComment"]);
                                leaveRequestModel.RequestedOnDate = dataSet.Tables[0].Rows[i]["CreateDate"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CreateDate"]);
                                leaveRequestModels.Add(leaveRequestModel);
                            }
                        }
                        var pager = new CustomPagination((dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0 && dataSet.Tables[1].Columns.Contains("TotalRecords") == true) ? Convert.ToInt32(dataSet.Tables[1].Rows[0]["TotalRecords"]) : 0, Page, PageSize);
                        leaveRequestCustom.leaveListing = leaveRequestModels;
                        leaveRequestCustom.CustomPagination = pager;
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return leaveRequestCustom;
        }

        public async Task<long> LeaveVerification(string leaveRequestId, string approveRejectstatus, string approveRejectComment, string approveRejectBy)
        {
            try
            {
                long result = 0;
                using (var dbConnect = connectionFactory.GetDAL)
                {
                    SqlParameter[] sqlparameters =
                    {
                    new SqlParameter("@intbLeaveRequestId",SqlDbType.BigInt){ Value = leaveRequestId},
                    new SqlParameter("@chvnApproveRejectStatus", SqlDbType.NVarChar) { Value = approveRejectstatus },
                    new SqlParameter("@chvnApproveRejectComment", SqlDbType.NVarChar) { Value = approveRejectComment },
                    new SqlParameter("@intbApproveRejectBy",SqlDbType.BigInt){ Value = approveRejectBy},
                    new SqlParameter("@chvnOperationType",SqlDbType.VarChar){ Value = "ApproveRejectLeave"}
                    };
                    result = await Task.Run(() => dbConnect.SPExecuteScalarReturnValue("[WebApplication_SP].[usp_Supervisor_LeaveRequest_ApproveReject_SelectAll_SelectById]", sqlparameters));
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
