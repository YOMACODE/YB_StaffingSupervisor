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

        public Task<DataTable> ExportClaimRequestsList(string SearchUserCode, string SearchAssociateName, string SearchEmail, string SearchMobileNumber, string SearchStatus)
        {
            throw new NotImplementedException();
        }




        public async Task<ClaimRequestsCustom> GetClaimRequestsListing(int Page, int PageSize, ClaimRequestsCustom SearchRequest)
        {
            ClaimRequestsCustom claimRequestsCustom = new ClaimRequestsCustom();
            try
            {
                using (var dbconnect = connectionFactory.GetDAL)
                {
                    SqlParameter[] sqlparameters =
                    {
                    new SqlParameter("@intbSupervisorId",SqlDbType.BigInt){Value = SearchRequest.SupervisorId},
                    new SqlParameter("@chvnSearchUserCode",SqlDbType.VarChar){Value = SearchRequest.SearchUserCode},
                    new SqlParameter("@chvnSearchMobileNumber",SqlDbType.VarChar){Value = SearchRequest.SearchMobileNumber},
                    new SqlParameter("@chvnSearchAssociateName",SqlDbType.VarChar){Value = SearchRequest.SearchAssociateName},
                    new SqlParameter("@chvnSearchEmailId",SqlDbType.VarChar){Value = SearchRequest.SearchEmail},
                    new SqlParameter("@intOffsetValue",SqlDbType.Int){ Value=(Page-1) * PageSize },
                    new SqlParameter("@intPagingSize",SqlDbType.Int){ Value=PageSize },
                    new SqlParameter("@chvnSortOrderBy", SqlDbType.NVarChar,10) { Value = SearchRequest.SortOrderBy},
                    new SqlParameter("@chvnSortColumnName", SqlDbType.NVarChar,512) { Value = SearchRequest.SortColumnName},
                    new SqlParameter("@chvnOperationType", SqlDbType.VarChar) { Value = "SELECTCLAIM" },
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
                                claimRequestsModel.YomaId = dataSet.Tables[0].Rows[i]["UserCode"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["UserCode"]);
                                claimRequestsModel.UserId = dataSet.Tables[0].Rows[i]["UserId"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["UserId"]);
                                claimRequestsModel.AssociateName = dataSet.Tables[0].Rows[i]["FullName"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["FullName"]);
                                claimRequestsModel.Email = dataSet.Tables[0].Rows[i]["EmailId"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["EmailId"]);
                                claimRequestsModel.MobileNumber = dataSet.Tables[0].Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["MobileNumber"]);
                                claimRequestsModel.ShowClaim = dataSet.Tables[0].Rows[i]["ShowClaim"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ShowClaim"]);
                                claimRequestsModels.Add(claimRequestsModel);
                            }
                        }
                        var pager = new CustomPagination((dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0 && dataSet.Tables[1].Columns.Contains("TotalRecords") == true) ? Convert.ToInt32(dataSet.Tables[1].Rows[0]["TotalRecords"]) : 0, Page, PageSize);
                        claimRequestsCustom.ClaimRequestListing = claimRequestsModels;
                        claimRequestsCustom.CustomPagination = pager;
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return claimRequestsCustom;
        }

        public async Task<ClaimRequestsCustom> GetUserRequestsListing(int Page, int PageSize, ClaimRequestsCustom SearchRequest1)
        {
            ClaimRequestsCustom claimRequestsCustom = new ClaimRequestsCustom();
            try
            {
                using (var dbconnect = connectionFactory.GetDAL)
                {
                    SqlParameter[] sqlparameters =
                    {
                    new SqlParameter("@intUserid",SqlDbType.BigInt){Value = SearchRequest1.UserId},
                    new SqlParameter("@chvnsearchClaimType",SqlDbType.VarChar){Value = SearchRequest1.SearchClaimType},
                    new SqlParameter("@chvnSearchStatusType",SqlDbType.VarChar){Value = SearchRequest1.SearchStatus},
                      new SqlParameter("@Month",SqlDbType.VarChar){Value = SearchRequest1.SearchMonth},
                    new SqlParameter("@Year",SqlDbType.VarChar){Value = SearchRequest1.SearchYear},
                    new SqlParameter("@intOffsetValue",SqlDbType.Int){ Value=(Page-1) * PageSize },
                    new SqlParameter("@intPagingSize",SqlDbType.Int){ Value=PageSize },
                    new SqlParameter("@chvnSortOrderBy", SqlDbType.NVarChar,10) { Value = SearchRequest1.SortOrderBy},
                    new SqlParameter("@chvnSortColumnName", SqlDbType.NVarChar,512) { Value = SearchRequest1.SortColumnName},
                    new SqlParameter("@chvnOperationType", SqlDbType.VarChar) { Value = "USERCLAIM" },
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
                                claimRequestsModel.ClaimInitiateDate = dataSet.Tables[0].Rows[i]["ClaimInitiateDate"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["ClaimInitiateDate"]);
                                claimRequestsModel.DistanceTravelled = dataSet.Tables[0].Rows[i]["DistanceTravelled"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["DistanceTravelled"]);
                                claimRequestsModel.DistanceCharge = dataSet.Tables[0].Rows[i]["DistanceCharge"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["DistanceCharge"]);
                                claimRequestsModel.ViewRouteMap = dataSet.Tables[0].Rows[i]["ViewRouteMap"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ViewRouteMap"]);
                                claimRequestsModel.ClaimType = dataSet.Tables[0].Rows[i]["ClaimType"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ClaimType"]);
                                claimRequestsModel.ClaimAmount = dataSet.Tables[0].Rows[i]["ClaimAmount"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ClaimAmount"]);
                                claimRequestsModel.Remark = dataSet.Tables[0].Rows[i]["Remark"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["Remark"]);
                                claimRequestsModel.ClaimSupportingImagePath = dataSet.Tables[0].Rows[i]["ClaimSupportingImagePath"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ClaimSupportingImagePath"]);
                                claimRequestsModel.ApproveRejectComment = dataSet.Tables[0].Rows[i]["ApproveRejectComment"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectComment"]);
                                claimRequestsModel.ApproveRejectStatus = dataSet.Tables[0].Rows[i]["ApproveRejectStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectStatus"]);
                                claimRequestsModel.UserId = dataSet.Tables[0].Rows[i]["UserId"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["UserId"]);
                                claimRequestsModel.AdditionalStatus = dataSet.Tables[0].Rows[i]["AdditionalStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["AdditionalStatus"]);

                                claimRequestsModel.ClaimRequestId = dataSet.Tables[0].Rows[i]["ClaimRequestId"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ClaimRequestId"]);
                                claimRequestsModels.Add(claimRequestsModel);
                            }
                        }
                        var pager = new CustomPagination((dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0 && dataSet.Tables[1].Columns.Contains("TotalRecords") == true) ? Convert.ToInt32(dataSet.Tables[1].Rows[0]["TotalRecords"]) : 0, Page, PageSize);
                        claimRequestsCustom.ClaimRequestListing = claimRequestsModels;
                        claimRequestsCustom.CustomPagination = pager;
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return claimRequestsCustom;
        }


        public async Task<long> ClaimApproveReject(string claimRequestId, string approveRejectstatus, string approveRejectComment, string approveRejectBy)
        {
            try
            {
                long result = 0;
                using (var dbConnect = connectionFactory.GetDAL)
                {
                    SqlParameter[] sqlparameters =
                    {
                    new SqlParameter("@intClaimRequestId",SqlDbType.BigInt){ Value = claimRequestId},
                    new SqlParameter("@chvnApprovedRejectStatus", SqlDbType.NVarChar) { Value = approveRejectstatus },
                    new SqlParameter("@chvnApproveRejectComment", SqlDbType.NVarChar) { Value = approveRejectComment },
                    new SqlParameter("@intuserid",SqlDbType.BigInt){ Value = approveRejectBy},
                    new SqlParameter("@chvnOperationType",SqlDbType.VarChar){ Value = "ApproveReject"}
                    };
                    result = await Task.Run(() => dbConnect.SPExecuteScalarReturnValue("[WebApplication_SP].[usp_Supervisiour_Approval_ClaimRequest_Select_Insert_Update]", sqlparameters));
                }
                return result;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return 0;
            }
        }


        public async Task<DataSet> ExportUserClaimrequestList(string Userid, string Month, string Year)
        {
            using (var dbconnect = connectionFactory.GetDAL)
            {
                SqlParameter[] sqlparameters =
                {
                    new SqlParameter("@intUserid", SqlDbType.NVarChar) { Value = Userid},
                    new SqlParameter("@Month", SqlDbType.NVarChar) { Value = Month},
                    new SqlParameter("@Year", SqlDbType.BigInt) { Value = Year },
                };
                DataSet dataTable = await Task.Run(() => dbconnect.SPExecuteDataset("[WebApplication_SP].[usp_Download_User_ClaimRequest_Report]", sqlparameters, "dt"));

                return dataTable;
            }
        }
    }
}
