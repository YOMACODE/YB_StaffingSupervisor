using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Common;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;
using YB_StaffingSupervisor.DataAccess.Entities.Model;
using YB_StaffingSupervisor.DataAccess.Infrastructure;
using YB_StaffingSupervisor.DataAccess.Contract;

namespace YB_StaffingSupervisor.DataAccess.Repository
{
    public class UserClaimRequestsRepository : BaseRepository, IUserClaimRequestsRepository
    {
        public UserClaimRequestsRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
        public async Task<UserClaimRequetsCustom> GetUserClaimRequestListing(int Page, int PageSize, UserClaimRequetsCustom SearchRequest)
        {
            UserClaimRequetsCustom userClaimRequetsCustom = new UserClaimRequetsCustom();
            using (var dbconnect = connectionFactory.GetDAL)
            {
                SqlParameter[] sqlparameters =
                {
                    //new SqlParameter("@intOffsetValue",SqlDbType.Int){ Value=(Page-1) * PageSize },
                    //new SqlParameter("@intPagingSize",SqlDbType.Int){ Value=PageSize },
                    //new SqlParameter("@chvnSortOrderBy", SqlDbType.NVarChar,512) { Value = SearchRequest.SortOrderBy},
                    //new SqlParameter("@chvnSortColumnName", SqlDbType.NVarChar) { Value = SearchRequest.SortColumnName},
                    //new SqlParameter("", SqlDbType.NVarChar) { Value = SearchRequest.SearchClaimType},
                    //new SqlParameter("@chvnName", SqlDbType.NVarChar, 16) { Value = SearchRequest.SearchClaimStatus },
                    //new SqlParameter("@chvnSearchEmailId", SqlDbType.NVarChar, 16) { Value = SearchRequest.SearchDate },
                   // new SqlParameter("@chvnSearchMobileNumber", SqlDbType.NVarChar, 16) { Value = SearchRequest.SearchYear },
                    //new SqlParameter("@chvnSearchMobileNumber", SqlDbType.NVarChar, 16) { Value = SearchRequest.SearchMonth },
                    //new SqlParameter("@intMonth", SqlDbType.Int) { Value = SearchRequest.SearchMonth },
                    //new SqlParameter("@intYear", SqlDbType.Int) { Value = SearchRequest.SearchYear },
                    new SqlParameter("@chvnOperationType", SqlDbType.NVarChar) { Value = "EXPORTAUDITAPPROVAL" },
                };
                List<UserClaimRequestsModel> userClaimRequestsModels = new List<UserClaimRequestsModel>();
                DataSet dataSet = await Task.Run(() => dbconnect.SPExecuteDataset("[WebApplication_SP].[usp_Supervisiour_Approval_ClaimRequest_Select_Insert_Update]", sqlparameters, "dataSet"));
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {
                            UserClaimRequestsModel userClaimRequestsModel = new UserClaimRequestsModel();
                            //claimRequestsModel.ClaimDate = dataSet.Tables[0].Rows[i]["UserCode"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["UserCode"]);
                            //claimRequestsModel. = dataSet.Tables[0].Rows[i]["LeaveRequestId"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["LeaveRequestId"]);
                          //  userClaimRequestsModel.SNo = dataSet.Tables[0].Rows[i]["S.No."] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["S.No."]);
                            userClaimRequestsModel.Date = dataSet.Tables[0].Rows[i]["CreateDate"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["CreateDate"]);
                            userClaimRequestsModel.DistanceTravelled = dataSet.Tables[0].Rows[i]["Distance Travelled (KM)"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["Distance Travelled (KM)"]);
                            userClaimRequestsModel.DistanceCharge = dataSet.Tables[0].Rows[i]["Distance Charge"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["Distance Charge"]);
                            userClaimRequestsModel.ViewRouteMap = dataSet.Tables[0].Rows[i]["View Route Map"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["View Route Map"]);
                            userClaimRequestsModel.ClaimType = dataSet.Tables[0].Rows[i]["ClaimType"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["ClaimType"]);
                            userClaimRequestsModel.ClaimAmount = dataSet.Tables[0].Rows[i]["ClaimAmount"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["ClaimAmount"]);
                            userClaimRequestsModel.Description = dataSet.Tables[0].Rows[i]["Remark"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["Remark"]);
                            userClaimRequestsModel.ViewSupporting = dataSet.Tables[0].Rows[i]["ClaimSupportingImagePath"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["ClaimSupportingImagePath"]);
                            userClaimRequestsModel.Comment = dataSet.Tables[0].Rows[i]["ApproveRejectComment"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectComment"]);
                            userClaimRequestsModel.ClaimStatus = dataSet.Tables[0].Rows[i]["ApproveRejectStatus"] == DBNull.Value ? Convert.ToString("") : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectStatus"]);
                            userClaimRequestsModels.Add(userClaimRequestsModel);
                        }
                    }
                  //  var pager = new CustomPagination((dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0 && dataSet.Tables[1].Columns.Contains("TotalRecords") == true) ? Convert.ToInt32(dataSet.Tables[1].Rows[0]["TotalRecords"]) : 0, Page, PageSize);
                    userClaimRequetsCustom.UserClaimRequestListing = userClaimRequestsModels;
                  //  userClaimRequetsCustom.CustomPagination = pager;
                }
            }
            return userClaimRequetsCustom;
        }

    }
}
