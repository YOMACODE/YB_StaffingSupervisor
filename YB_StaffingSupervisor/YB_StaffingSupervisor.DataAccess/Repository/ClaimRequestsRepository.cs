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
	public class ClaimRequestsRepository : BaseRepository, IClaimRequestsRepository
	{
		public ClaimRequestsRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
		{

		}
		public async Task<ClaimRequestsCustom> GetClaimRequestsListing(int Page, int PageSize, ClaimRequestsCustom SearchRequest)
		{
			ClaimRequestsCustom claimRequestsCustom = new ClaimRequestsCustom();
			using (var dbconnect = connectionFactory.GetDAL)
			{
				SqlParameter[] sqlparameters =
				{
					new SqlParameter("@intOffsetValue",SqlDbType.Int){ Value=(Page-1) * PageSize },
					new SqlParameter("@intPagingSize",SqlDbType.Int){ Value=PageSize },
					new SqlParameter("@chvnSortOrderBy", SqlDbType.NVarChar,512) { Value = SearchRequest.SortOrderBy},
					new SqlParameter("@chvnSortColumnName", SqlDbType.NVarChar) { Value = SearchRequest.SortColumnName},
					new SqlParameter("@chvnSearchUserCode", SqlDbType.NVarChar) { Value = SearchRequest.SearchUserCode},
					new SqlParameter("@chvnName", SqlDbType.NVarChar, 16) { Value = SearchRequest.SearchAssociateName },
					new SqlParameter("@chvnSearchEmailId", SqlDbType.NVarChar, 16) { Value = SearchRequest.SearchEmail },
					new SqlParameter("@chvnSearchMobileNumber", SqlDbType.NVarChar, 16) { Value = SearchRequest.SearchMobileNumber },
                    //new SqlParameter("@chvnAppliedDate", SqlDbType.NVarChar, 16) { Value = SearchRequest.SearchAppliedDate },
                    new SqlParameter("@chvnOperationType", SqlDbType.NVarChar) { Value = "SELECTCLAIM" },
				};
				List<ClaimRequestsModel> claimRequestsModels = new List<ClaimRequestsModel>();
				DataSet dataSet = await Task.Run(() => dbconnect.SPExecuteDataset("[WebApplication_SP].[usp_Supervisiour_Approval_ClaimRequest_Select_Insert_Update]", sqlparameters, "dataSet"));
				if (dataSet != null && dataSet.Tables.Count > 0)
				{
					if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
					{
						for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
						{
							ClaimRequestsModel claimRequestsModel = new ClaimRequestsModel();
							//claimRequestsModel.ClaimDate = dataSet.Tables[0].Rows[i]["UserCode"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["UserCode"]);
							//claimRequestsModel. = dataSet.Tables[0].Rows[i]["LeaveRequestId"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["LeaveRequestId"]);
							claimRequestsModel.SNo = dataSet.Tables[0].Rows[i]["S.No."] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["S.No."]);
							claimRequestsModel.YomaId = dataSet.Tables[0].Rows[i]["usercode"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["usercode"]);
							claimRequestsModel.AssociateName = dataSet.Tables[0].Rows[i]["fullname"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["fullname"]);
							claimRequestsModel.Email = dataSet.Tables[0].Rows[i]["Emailid"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["Emailid"]);
							claimRequestsModel.MobileNumber = dataSet.Tables[0].Rows[i]["mobilenumber"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["mobilenumber"]);
							//claimRequestsModel.ClaimType = dataSet.Tables[0].Rows[i]["LeaveToDate"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["LeaveToDate"]);
							//claimRequestsModel.ClaimAmount = dataSet.Tables[0].Rows[i]["LeaveType"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["LeaveType"]);
							//claimRequestsModel.Supporting = dataSet.Tables[0].Rows[i]["Days"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["Days"]);
							//claimRequestsModel.Status = dataSet.Tables[0].Rows[i]["ApproveRejectStatus"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectStatus"]);
							//claimRequestsModel.Comment = dataSet.Tables[0].Rows[i]["ApproveRejectComment"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectComment"]);
							//claimRequestsModel.Approvedby = dataSet.Tables[0].Rows[i]["ApproveRejectBy"] == DBNull.Value ? Convert.ToString("") : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectBy"]);
							claimRequestsModels.Add(claimRequestsModel);
						}
					}
					var pager = new CustomPagination((dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0 && dataSet.Tables[1].Columns.Contains("TotalRecords") == true) ? Convert.ToInt32(dataSet.Tables[1].Rows[0]["TotalRecords"]) : 0, Page, PageSize);
					claimRequestsCustom.ClaimRequestListing = claimRequestsModels;
					claimRequestsCustom.CustomPagination = pager;
				}
			}
			return claimRequestsCustom;
		}

        public async Task<DataTable> ExportClaimRequestsList(string SearchUserCode, string SearchAssociateName, string SearchEmail, string SearchMobileNumber, string SearchStatus)
        {
            using (var dbconnect = connectionFactory.GetDAL)
            {
                SqlParameter[] sqlparameters =
                {
                    new SqlParameter("@chvnSearchUserCode", SqlDbType.NVarChar) { Value =  SearchUserCode},
                    new SqlParameter("@chvnSearchFullName", SqlDbType.NVarChar) { Value = SearchAssociateName},
                    new SqlParameter("@chvnSearchFullName", SqlDbType.NVarChar) { Value = SearchEmail},
                    new SqlParameter("@chvnSearchFullName", SqlDbType.NVarChar) { Value = SearchMobileNumber},
                    new SqlParameter("@chvnSearchFullName", SqlDbType.NVarChar) { Value = SearchStatus},

                };
                DataTable dataTable = await Task.Run(() => dbconnect.SPExecuteDataTable("[WebApplication_SP].[usp_DownloadLaveApprovalRejectReport_New]", sqlparameters, "dt"));

                return dataTable;
            }
        }


    }
}
