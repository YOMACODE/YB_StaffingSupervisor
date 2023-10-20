using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using YB_AssessmentManagement.DataAccess.Contract;
using YB_AssessmentManagement.DataAccess.Entities;
using YB_AssessmentManagement.DataAccess.Infrastructure;

namespace YB_AssessmentManagement.DataAccess.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        #region Token validation 
        public int SaveLoginToken(string username, string token, DateTime tokenExpireOn)
        {
            try
            {
                using var dbconnect = connectionFactory.GetDAL;
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@chvnLoginId", SqlDbType.NVarChar) { Value = username },
                    new SqlParameter("@chvnToken", SqlDbType.NVarChar, -1) { Value = token },
                    new SqlParameter("@TokenExpiresOn", SqlDbType.DateTime) { Value = tokenExpireOn.ToString() }
                };
                int result = Convert.ToInt32(dbconnect.SPExecuteScalar("[WebApplication_SP].[usp_Save_User_LoginToken]", sqlParameters));
                return result;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return 0;
            }
        }
        public bool ValidateClientUserToken(string username, string token, DateTime tokenExpireOn, string actionUrl)
        {
            try
            {
                using var dbconnect = connectionFactory.GetDAL;
                SqlParameter[] sqlParameters =
                {
                  new SqlParameter("@chvnUserName", SqlDbType.NVarChar) { Value = username },
                  new SqlParameter("@chvnToken", SqlDbType.NVarChar, -1) { Value = token },
                  new SqlParameter("@TokenExpireOn", SqlDbType.NVarChar) { Value = tokenExpireOn.ToString()},
                  new SqlParameter("@chvnActionUrl", SqlDbType.NVarChar,-1) { Value = actionUrl}
                };
                bool result = Convert.ToBoolean(dbconnect.SPExecuteScalar("[WebApplication_SP].[usp_ValidateClientUser_Token]", sqlParameters));
                return result;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }
        public bool ClearToken(long UserId)
        {
            try
            {
                using var dbconnect = connectionFactory.GetDAL;
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@bintUserId", SqlDbType.BigInt) { Value = UserId }
                };
                bool result = Convert.ToBoolean(dbconnect.SPExecuteScalar("[WebApplication_SP].[usp_ClearToken]", sqlParameters));
                return result;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }
        #endregion

        #region check user login based on username and password
        public LoginModel CheckLogin(string username, string password)
        {
            LoginModel loginModel = new LoginModel();
            try
            {
                using var dbconnect = connectionFactory.GetDAL;
                SqlParameter[] sqlParameters =
                {
                        new SqlParameter("@chvnLoginId", SqlDbType.VarChar) { Value = username },
                        new SqlParameter("@chvnPassword", SqlDbType.VarChar) { Value = password },
                       new SqlParameter("@intModuleId", SqlDbType.VarChar) { Value = 2 }
                };
                var loginUser = dbconnect.SPExecuteDataTable("[WebApplication_SP].[usp_User_Login_ById_Token]", sqlParameters, "dt");
                //user not found
                if (loginUser == null || loginUser.Rows.Count == 0)
                {
                    return null;
                }
                else if (loginUser.Columns.Count == 1)
                {
                    loginModel.Message = loginUser.Rows[0][0].ToString();
                    return loginModel;
                }
                loginModel.UserName = loginUser.Rows[0]["UserName"] == DBNull.Value ? string.Empty : Convert.ToString(loginUser.Rows[0]["UserName"]);
                loginModel.Password = loginUser.Rows[0]["Password"] == DBNull.Value ? string.Empty : Convert.ToString(loginUser.Rows[0]["Password"]);
                loginModel.UserId = loginUser.Rows[0]["UserId"] == DBNull.Value ? 0 : Convert.ToInt32(loginUser.Rows[0]["UserId"]);
                loginModel.MenuList = loginUser.Rows[0]["MenuList"] == DBNull.Value ? string.Empty : Convert.ToString(loginUser.Rows[0]["MenuList"]);
                return loginModel;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return null;
            }
        }
        #endregion

        #region Bind user setting based on userId
        public UserSettingModel UserSetting(long? UserId)
        {
            UserSettingModel userSetting = new UserSettingModel();
            using (var dbconnect = connectionFactory.GetDAL)
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@inbUserId", SqlDbType.BigInt) { Value=UserId},
                };
                DataTable dataTable = dbconnect.SPExecuteDataTable("[WebApplication_SP].[usp_User_Setting_ById]", sqlParameters, "dataTable");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    userSetting.UserId = dataTable.Rows[0]["UserId"] == DBNull.Value ? "0" : Convert.ToString(dataTable.Rows[0]["UserId"]);
                    userSetting.ClientId = dataTable.Rows[0]["ClientId"] == DBNull.Value ? "0" : Convert.ToString(dataTable.Rows[0]["ClientId"]);
                    userSetting.ClientName = dataTable.Rows[0]["ClientName"] == DBNull.Value ? string.Empty : Convert.ToString(dataTable.Rows[0]["ClientName"]);
                    userSetting.WebThemeCode = dataTable.Rows[0]["WebThemeCode"] == DBNull.Value ? string.Empty : Convert.ToString(dataTable.Rows[0]["WebThemeCode"]);
                    userSetting.IsCollapseSidebarEnabled = dataTable.Rows[0]["IsCollapseSidebarEnabled"] == DBNull.Value ? "full" : (Convert.ToBoolean(dataTable.Rows[0]["IsCollapseSidebarEnabled"]) == true ? "mini-sidebar" : "full");
                    userSetting.IsFixedSidebarEnabled = dataTable.Rows[0]["IsFixedSidebarEnabled"] == DBNull.Value ? "false" : (Convert.ToBoolean(dataTable.Rows[0]["IsFixedSidebarEnabled"]) == true ? "true" : "false");
                    userSetting.IsFixedHeaderEnabled = dataTable.Rows[0]["IsFixedHeaderEnabled"] == DBNull.Value ? "false" : (Convert.ToBoolean(dataTable.Rows[0]["IsFixedHeaderEnabled"]) == true ? "true" : "false");
                    userSetting.IsBoxedLayoutEnabled = dataTable.Rows[0]["IsBoxedLayoutEnabled"] == DBNull.Value ? "false" : (Convert.ToBoolean(dataTable.Rows[0]["IsBoxedLayoutEnabled"]) == true ? "true" : "false");
                }
                else
                {
                    userSetting = null;
                }
            }
            return userSetting;
        }
        #endregion

        #region Update the user theme based on themaname and userId
        public void ChangeUserTheme(long? UserId, string ThemeName)
        {
            using var dbconnect = connectionFactory.GetDAL;
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@inbUserId", SqlDbType.BigInt) { Value=UserId},
                new SqlParameter("@chvnType", SqlDbType.NVarChar) { Value="THEME"},
                new SqlParameter("@chvnSetting", SqlDbType.NVarChar) { Value=ThemeName},
            };
            _ = dbconnect.SPExecuteNonQuery("[WebApplication_SP].[usp_User_Update_Setting_ById]", sqlParameters);
        }
        #endregion

        #region change layout of userid and type
        public void ChangeUserLayout(long? UserId, string Type)
        {
            using var dbconnect = connectionFactory.GetDAL;
            SqlParameter[] sqlParameters =
             {
                new SqlParameter("@inbUserId", SqlDbType.BigInt) { Value=UserId},
                new SqlParameter("@chvnType", SqlDbType.NVarChar) { Value=Type},
             };
            _ = dbconnect.SPExecuteNonQuery("[WebApplication_SP].[usp_User_Update_Setting_ById]", sqlParameters);
        }
        #endregion

        #region user detail based on userId
        public UserModel User(long? UserId)
        {
            UserModel user = new UserModel();
            using (var dbconnect = connectionFactory.GetDAL)
            {
                SqlParameter[] sqlparameters =
                {
                    new SqlParameter("@inbUserId", SqlDbType.BigInt) { Value=UserId},
                };
                DataTable dataTable = dbconnect.SPExecuteDataTable("[WebApplication_SP].[usp_User_Profile_ById]", sqlparameters, "dataTable");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    user.UserId = dataTable.Rows[0]["UserId"] == DBNull.Value ? 0 : Convert.ToInt64(dataTable.Rows[0]["UserId"]);
                    user.ClientId = dataTable.Rows[0]["ClientId"] == DBNull.Value ? 0 : Convert.ToInt32(dataTable.Rows[0]["ClientId"]);
                    user.UserName = dataTable.Rows[0]["UserName"] == DBNull.Value ? string.Empty : Convert.ToString(dataTable.Rows[0]["UserName"]);
                    user.RoleName = dataTable.Rows[0]["RoleName"] == DBNull.Value ? string.Empty : Convert.ToString(dataTable.Rows[0]["RoleName"]);
                    user.IsAdmin = dataTable.Rows[0]["IsAdmin"] != DBNull.Value && Convert.ToBoolean(dataTable.Rows[0]["IsAdmin"]);
                    user.CountryId = dataTable.Rows[0]["CountryId"] == DBNull.Value ? Convert.ToInt16(0) : Convert.ToInt16(dataTable.Rows[0]["CountryId"]);
                    user.RegionId = dataTable.Rows[0]["RegionId"] == DBNull.Value ? Convert.ToInt16(0) : Convert.ToInt16(dataTable.Rows[0]["RegionId"]);
                    user.RegionName = dataTable.Rows[0]["RegionName"] == DBNull.Value ? string.Empty : Convert.ToString(dataTable.Rows[0]["RegionName"]);
                    user.MobileNumber = dataTable.Rows[0]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dataTable.Rows[0]["MobileNumber"]);
                }
                else
                {
                    user = null;
                }
            }
            return user;
        }
        #endregion

        #region Bind dropdown in user list
        public async Task<IEnumerable<UserModel>> DropDownForUserList()
        {
            List<UserModel> userlist = new List<UserModel>();
            using var dbconnect = connectionFactory.GetDAL;
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@chvnType", SqlDbType.VarChar) { Value = "ALLUSER" },
            };
            DataTable dataTable = await Task.Run(() => dbconnect.SPExecuteDataTable("[WebApplication_SP].[usp_User_Update_Setting_ById]", sqlParameters, "dataTable"));
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    userlist.Add(new UserModel
                    {
                        UserName = Convert.ToString(dataTable.Rows[i]["UserName"]),
                        UserId = Convert.ToInt16(dataTable.Rows[i]["UserId"])
                    });
                }
                return userlist;
            }
            else
            {
                return userlist;
            }
        }
        #endregion
    }
}
