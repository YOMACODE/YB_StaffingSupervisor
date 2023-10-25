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
    public class OnDutyRequestRepository: BaseRepository,IOnDutyRequesteRepository
    {
        public OnDutyRequestRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

       
        public async Task<OnDutyRequestCustom> GetOnDutyRequestListing(int Page, int PageSize)
        {
            OnDutyRequestCustom onDutyRequestCustom = new OnDutyRequestCustom();
            using (var dbconnect = connectionFactory.GetDAL)
            {
                SqlParameter[] sqlparameters =
                {
                    new SqlParameter("@intOffsetValue",SqlDbType.Int){ Value=(Page-1) * PageSize },
                    new SqlParameter("@intPagingSize",SqlDbType.Int){ Value=PageSize },
                    new SqlParameter("@intUserId",SqlDbType.Int){ Value=onDutyRequestCustom.SearchUserCode },
                    new SqlParameter("@chvnOperationType", SqlDbType.NVarChar) { Value = "LIST" },
                };
                List<OnDutyrequestModel> onDutyrequestModels = new List<OnDutyrequestModel>();
                DataSet dataSet = await Task.Run(() => dbconnect.SPExecuteDataset("[WebApplication_SP].[usp_CreateOnDutyrequest]", sqlparameters, "ds"));
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {
                            OnDutyrequestModel onDutyrequestModel = new OnDutyrequestModel();
                            onDutyrequestModel.Username = dataSet.Tables[0].Rows[i]["FullName"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["FullName"]);
                            onDutyrequestModel.Date = dataSet.Tables[0].Rows[i]["CreateDate"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["CreateDate"]);
                            onDutyrequestModel.Status = dataSet.Tables[0].Rows[i]["ApproveRejectStatus"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectStatus"]);
                            onDutyrequestModel.YomaId = dataSet.Tables[0].Rows[i]["UserCode"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["UserCode"]);
                            onDutyrequestModel.Remark = dataSet.Tables[0].Rows[i]["Remark"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["Remark"]);
                            onDutyrequestModel.RequestType = dataSet.Tables[0].Rows[i]["OnDutyType"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["OnDutyType"]);
                            onDutyrequestModel.RequestedOn = dataSet.Tables[0].Rows[i]["OnDutyDate"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["OnDutyDate"]);
                            onDutyrequestModel.ApprovedBy = dataSet.Tables[0].Rows[i]["ApproveRejectBy"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectBy"]);
                            onDutyrequestModel.Comment = dataSet.Tables[0].Rows[i]["ApproveRejectComment"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectComment"]);
                            onDutyrequestModel.DailyAttendanceOnDutyRequestId = dataSet.Tables[0].Rows[i]["DailyAttendanceOnDutyRequestId"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["DailyAttendanceOnDutyRequestId"]);
                            onDutyrequestModels.Add(onDutyrequestModel);
                        }
                    }
                    var pager = new CustomPagination((dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0 && dataSet.Tables[1].Columns.Contains("TotalRecords") == true) ? Convert.ToInt32(dataSet.Tables[1].Rows[0]["TotalRecords"]) : 0, Page, PageSize);
                    onDutyRequestCustom.OnDutyList = onDutyrequestModels;
                    onDutyRequestCustom.CustomPagination = pager;
                }
            }
            return onDutyRequestCustom;
        }

        public async Task<long> InsertOnDutyRequest(string DailyAttendanceOnDutyRequestId, string Comment, string Status, string UserId)
        {
            long result=0;
            using (var dbconnect = connectionFactory.GetDAL)
            {
                SqlParameter[] sqlParameters =
             {
                 new SqlParameter("@intUserId",SqlDbType.NVarChar){ Value=UserId },
                 new SqlParameter("@intDailyAttendanceOnDutyRequestId",SqlDbType.NVarChar){ Value=DailyAttendanceOnDutyRequestId },
                 new SqlParameter("@bitStatus",SqlDbType.BigInt) { Value = Status },
                 new SqlParameter("@chvnComment",SqlDbType.NVarChar){ Value=Comment },
                 new SqlParameter("@chvnOperationType",SqlDbType.NVarChar) { Value ="UPDATE" },
             };
                result = dbconnect.SPExecuteScalar("[WebApplication_SP].[usp_CreateOnDutyrequest]", sqlParameters);
            }
            return result;
        }
         
       
    }
}
