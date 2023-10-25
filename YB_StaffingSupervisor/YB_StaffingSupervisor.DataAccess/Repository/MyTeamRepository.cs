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
	public class MyTeamRepository: BaseRepository,IMyTeamRepository
	{
		public MyTeamRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
		{

		}
		public async Task<TeamMemberCustom> GetTeamMembersListing(int Page, int PageSize, TeamMemberCustom SearchRequest)
		{
			TeamMemberCustom teamMemberCustom = new TeamMemberCustom();
			using (var dbconnect = connectionFactory.GetDAL)
			{
				SqlParameter[] sqlparameters =
				{
					new SqlParameter("@intOffsetValue",SqlDbType.Int){ Value=(Page-1) * PageSize },
					new SqlParameter("@intPagingSize",SqlDbType.Int){ Value=PageSize },
					new SqlParameter("@chvnSortOrderBy", SqlDbType.NVarChar,512) { Value = SearchRequest.SortOrderBy},
					new SqlParameter("@chvnSortColumnName", SqlDbType.NVarChar) { Value = SearchRequest.SortColumnName},
					new SqlParameter("@chvnSearchUserCode", SqlDbType.NVarChar) { Value = SearchRequest.SearchUserCode},
					new SqlParameter("@chvnSearchFullName", SqlDbType.NVarChar, 16) { Value = SearchRequest.SearchFullName },
					new SqlParameter("@chvnSearchMobileNumber", SqlDbType.NVarChar, 16) { Value = SearchRequest.SearchMobileNumber },
					new SqlParameter("@dtmSearchJoiningDate", SqlDbType.NVarChar,16) { Value = SearchRequest.SearchJoiningDate },
					new SqlParameter("@chvnSearchEmailId", SqlDbType.NVarChar) { Value = SearchRequest.SearchEmailId },
					new SqlParameter("@chvnSearchDesignation", SqlDbType.BigInt) { Value = SearchRequest.SearchDesignation },
					new SqlParameter("@chvnOperationType", SqlDbType.NVarChar) { Value = "SELECTAll" },
				};
				List<TeamMemberModel> teamMemberModels = new List<TeamMemberModel>();
				DataSet dataSet = await Task.Run(() => dbconnect.SPExecuteDataset("[WebApplication_SP].[usp_SupervisorTeam_GetList]", sqlparameters, "dataSet"));
				if (dataSet != null && dataSet.Tables.Count > 0)
				{
					if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
					{
						for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
						{
							TeamMemberModel teamMemberModel = new TeamMemberModel();
							teamMemberModel.UserCode = dataSet.Tables[0].Rows[i]["UserCode"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["UserCode"]);
							teamMemberModel.FullName = dataSet.Tables[0].Rows[i]["FullName"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["FullName"]);
							teamMemberModel.EmailId = dataSet.Tables[0].Rows[i]["EmailId"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["EmailId"]);
							teamMemberModel.MobileNumber = dataSet.Tables[0].Rows[i]["MobileNumber"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["MobileNumber"]);
							teamMemberModel.Designation = dataSet.Tables[0].Rows[i]["DesignationName"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["DesignationName"]);
							teamMemberModel.JoiningDate = dataSet.Tables[0].Rows[i]["JoiningDate"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["JoiningDate"]);
							teamMemberModels.Add(teamMemberModel);
						}
					}
					//var pager = new CustomPagination((dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0 && dataSet.Tables[1].Columns.Contains("TotalRecords") == true) ? Convert.ToInt32(dataSet.Tables[1].Rows[0]["TotalRecords"]) : 0, Page, PageSize);
					teamMemberCustom.TeamMemberListing = teamMemberModels;
					//teamMemberCustom.CustomPagination = pager;
				}
			}
			return teamMemberCustom;
		}

		public async Task<DataTable> ExportTeamMemberList(string SearchUserCode, string SearchFullName, string SearchMobileNumber, string SearchEmailId, string SearchDesignation, string SearchJoiningDate)
		{
			using (var dbconnect = connectionFactory.GetDAL)
			{
				SqlParameter[] sqlparameters =
				{
					new SqlParameter("@chvnSearchUserCode", SqlDbType.NVarChar) { Value =  SearchUserCode},
					new SqlParameter("@chvnSearchFullName", SqlDbType.NVarChar) { Value = SearchFullName},
					new SqlParameter("@chvnSearchMobileNumber", SqlDbType.NVarChar) { Value = SearchMobileNumber },
					new SqlParameter("@chvnSearchEmailId", SqlDbType.NVarChar) { Value = SearchEmailId },
					new SqlParameter("@chvnSearchDesignation", SqlDbType.NVarChar) { Value = SearchDesignation },
					new SqlParameter("@dtmSearchJoiningDate", SqlDbType.NVarChar) { Value = SearchJoiningDate },
				};
				DataTable dataTable = await Task.Run(() => dbconnect.SPExecuteDataTable("[WebApplication_SP].[usp_DownloadTeamMemberReport_New]", sqlparameters, "dt"));

				return dataTable;
			}
		}
	}
}
