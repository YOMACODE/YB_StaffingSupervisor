using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Common;
using YB_StaffingSupervisor.DataAccess.Contract;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;
using YB_StaffingSupervisor.DataAccess.Entities.Model;
using YB_StaffingSupervisor.DataAccess.Infrastructure;

namespace YB_StaffingSupervisor.DataAccess.Repository
{
    public class LeaveApprovalsRepository : BaseRepository, ILeaveApprovalsRepository
    {
        public LeaveApprovalsRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
        public async Task<LeaveApprovalsCustom> GetLeaveApprovalsListing(int Page, int PageSize, LeaveApprovalsCustom SearchRequest)
        {
            LeaveApprovalsCustom leaveApprovalsCustom = new LeaveApprovalsCustom();
            using (var dbconnect = connectionFactory.GetDAL)
            {
                SqlParameter[] sqlparameters =
                {
                    new SqlParameter("@intOffsetValue",SqlDbType.Int){ Value=(Page-1) * PageSize },
                    new SqlParameter("@intPagingSize",SqlDbType.Int){ Value=PageSize },
                    new SqlParameter("@chvnSortOrderBy", SqlDbType.NVarChar,512) { Value = SearchRequest.SortOrderBy},
                    new SqlParameter("@chvnSortColumnName", SqlDbType.NVarChar) { Value = SearchRequest.SortColumnName},
                    new SqlParameter("@chvnUserCode", SqlDbType.NVarChar) { Value = SearchRequest.SearchUserCode},
                    new SqlParameter("@chvnAssociateName", SqlDbType.NVarChar, 16) { Value = SearchRequest.SearchAssociateName },
                    //new SqlParameter("@chvnAppliedDate", SqlDbType.NVarChar, 16) { Value = SearchRequest.SearchAppliedDate },
                    new SqlParameter("@chvnOperationType", SqlDbType.NVarChar) { Value = "GETLEAVEAPPROVALLIST" },
                };
                List<LeaveApprovalsModel> leaveApprovalsModels = new List<LeaveApprovalsModel>();
                DataSet dataSet = await Task.Run(() => dbconnect.SPExecuteDataset("[MobileApplication_SP].[usp_LeaveApproval_Insert_Update]", sqlparameters, "dataSet"));
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {
                            LeaveApprovalsModel leaveApprovalsModel = new LeaveApprovalsModel();
                            leaveApprovalsModel.UserCode = dataSet.Tables[0].Rows[i]["UserCode"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["UserCode"]);
                            leaveApprovalsModel.LeaveRequestId = dataSet.Tables[0].Rows[i]["LeaveRequestId"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["LeaveRequestId"]);
                            leaveApprovalsModel.AssociateName = dataSet.Tables[0].Rows[i]["FULLNAME"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["FULLNAME"]);
                            leaveApprovalsModel.StartDate = dataSet.Tables[0].Rows[i]["LeaveFromDate"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["LeaveFromDate"]);
                            leaveApprovalsModel.EndDate = dataSet.Tables[0].Rows[i]["LeaveToDate"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["LeaveToDate"]);
                            leaveApprovalsModel.LeaveType = dataSet.Tables[0].Rows[i]["LeaveType"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["LeaveType"]);
                            leaveApprovalsModel.LeaveDays = dataSet.Tables[0].Rows[i]["Days"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["Days"]);
                            leaveApprovalsModel.Status = dataSet.Tables[0].Rows[i]["ApproveRejectStatus"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectStatus"]);
                            leaveApprovalsModel.Comment = dataSet.Tables[0].Rows[i]["ApproveRejectComment"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectComment"]);
                            leaveApprovalsModel.Approvedby = dataSet.Tables[0].Rows[i]["ApproveRejectBy"] == DBNull.Value ? Convert.ToString("") : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectBy"]);
                            leaveApprovalsModels.Add(leaveApprovalsModel);
                        }
                    }
                    var pager = new CustomPagination((dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0 && dataSet.Tables[1].Columns.Contains("TotalRecords") == true) ? Convert.ToInt32(dataSet.Tables[1].Rows[0]["TotalRecords"]) : 0, Page, PageSize);
                    leaveApprovalsCustom.LeaveApprovalListing = leaveApprovalsModels;
                    leaveApprovalsCustom.CustomPagination = pager;
                }
            }
            return leaveApprovalsCustom;
        }


        public async Task<long> InsertApproveRejectLeave(string LeaveRequestId, string Status, string Comment, string UserId)
        {
            long result = 0;

            using (var dbconnect = connectionFactory.GetDAL)
            {
                SqlParameter[] sqlparameters =
                           {
                                new SqlParameter("@intLeaveRequestId", SqlDbType.NVarChar) { Value = LeaveRequestId },
                                new SqlParameter("@intUserId", SqlDbType.NVarChar) { Value = UserId },
                                new SqlParameter("@chvnStatus", SqlDbType.NVarChar) { Value = Status },
                                new SqlParameter("@chvnComment", SqlDbType.NVarChar) { Value = Comment },
                                new SqlParameter("@chvnOperationType", SqlDbType.NVarChar) { Value = "APPROVEREJECT" }
                           };
                result = await Task.Run(() => dbconnect.SPExecuteScalar("[MobileApplication_SP].[usp_LeaveApproval_Insert_Update]", sqlparameters));
            }
            return result;
        }

		public async Task<DataTable> ExportLeaveApprovalList(string SearchUserCode, string SearchAssociateName)
		{
			using (var dbconnect = connectionFactory.GetDAL)
			{
				SqlParameter[] sqlparameters =
				{
					new SqlParameter("@chvnSearchUserCode", SqlDbType.NVarChar) { Value =  SearchUserCode},
					new SqlParameter("@chvnSearchFullName", SqlDbType.NVarChar) { Value = SearchAssociateName},
				
				};
				DataTable dataTable = await Task.Run(() => dbconnect.SPExecuteDataTable("[WebApplication_SP].[usp_DownloadLaveApprovalRejectReport_New]", sqlparameters, "dt"));

				return dataTable;
			}
		}
	}
}
