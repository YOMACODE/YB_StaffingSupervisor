using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using YB_AssessmentManagement.DataAccess.Contract;
using YB_AssessmentManagement.DataAccess.Entities;
using YB_AssessmentManagement.DataAccess.Entities.Model;
using YB_AssessmentManagement.DataAccess.Infrastructure;

namespace YB_AssessmentManagement.DataAccess.Repository
{
    public class UserTokensRepository : BaseRepository, IUserTokensRepository
    {
        public UserTokensRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        #region Insert Or Update Token
        public async Task<long> InsertOrUpdateToken(UserTokenModel userTokenModel)
        {
            try
            {
                long result = 0;
                using (var dbconnect = connectionFactory.GetDAL)
                {
                    SqlParameter[] sqlParameters =
                    {
                        new SqlParameter("@chvnOperationType", SqlDbType.NVarChar,40) { Value ="INSERTORUPDATE" },
                        new SqlParameter("@intUserId", SqlDbType.BigInt) { Value = userTokenModel.UserId },

                        new SqlParameter("@chvnProvider", SqlDbType.NVarChar,512) { Value = userTokenModel.Provider},
                        new SqlParameter("@chvnTokenType", SqlDbType.NVarChar,512) { Value = userTokenModel.TokenType},
                        new SqlParameter("@chvnAccessToken", SqlDbType.NVarChar) { Value = userTokenModel.AccessToken},
                        new SqlParameter("@chvnRefreshToken", SqlDbType.NVarChar) { Value = userTokenModel.RefreshToken},
                        new SqlParameter("@intTokenExpiresIn", SqlDbType.Int) { Value = userTokenModel.TokenExpiresIn},
                        new SqlParameter("@chvnScope", SqlDbType.NVarChar,512) { Value = userTokenModel.Scope},

                        new SqlParameter("@intbCreatedBy", SqlDbType.BigInt) { Value = userTokenModel.CreatedBy}
                    };
                    result = await Task.Run(() => dbconnect.SPExecuteScalar("[WebApplication_SP].[usp_UserTokens_Insert_Update_Delete_SelectAll_SelectById]", sqlParameters));
                }
                return result;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return 0;
            }
        }
        #endregion

        #region Get Tokens List based on UserId & Provider
        public async Task<List<UserTokenModel>> GetTokensList(int UserId)
        {
            using var dbconnect = connectionFactory.GetDAL;
            SqlParameter[] sqlparameters =
            {
               new SqlParameter("@intUserId", SqlDbType.Int) { Value =Convert.ToInt32(UserId) },
               //new SqlParameter("@chvnProvider", SqlDbType.NVarChar,512) { Value =Provider },
               new SqlParameter("@chvnOperationType", SqlDbType.VarChar, 40) { Value = "SELECTALL" },
            };

            DataTable dt = await Task.Run(() => dbconnect.SPExecuteDataTable("[WebApplication_SP].[usp_UserTokens_Insert_Update_Delete_SelectAll_SelectById]", sqlparameters, "dt"));
            List<UserTokenModel> UserTokensModelList = new List<UserTokenModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserTokenModel UserTokenModel = new UserTokenModel
                    {
                        UserId = dt.Rows[i]["UserId"] == DBNull.Value ? string.Empty : Convert.ToString(dt.Rows[i]["UserId"]),
                        Provider = dt.Rows[i]["Provider"] == DBNull.Value ? string.Empty : Convert.ToString(dt.Rows[i]["Provider"]),
                        TokenType = dt.Rows[i]["TokenType"] == DBNull.Value ? string.Empty : Convert.ToString(dt.Rows[i]["TokenType"]),
                        AccessToken = dt.Rows[i]["AccessToken"] == DBNull.Value ? string.Empty : Convert.ToString(dt.Rows[i]["AccessToken"]),
                        RefreshToken = dt.Rows[i]["RefreshToken"] == DBNull.Value ? string.Empty : Convert.ToString(dt.Rows[i]["RefreshToken"]),
                        TokenExpiresIn = dt.Rows[i]["TokenExpiresIn"] == DBNull.Value ? Convert.ToInt32(0) : Convert.ToInt32(dt.Rows[i]["TokenExpiresIn"]),
                        Scope = dt.Rows[i]["Scope"] == DBNull.Value ? string.Empty : Convert.ToString(dt.Rows[i]["Scope"]),

                    };
                    UserTokensModelList.Add(UserTokenModel);
                }
            }
            return UserTokensModelList;
        }
        #endregion
    }
}
