﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Contract;
using YB_StaffingSupervisor.DataAccess.Entities;
using YB_StaffingSupervisor.DataAccess.Infrastructure;

namespace YB_StaffingSupervisor.DataAccess.Repository
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        #region user profile detail based on userId
        public async Task<UserProfileModel> UserProfile(string UserId)
        {
            UserProfileModel userProfile = new UserProfileModel();
            using (var dbconnect = connectionFactory.GetDAL)
            {
                SqlParameter[] sqlparameters =
                {
                    new SqlParameter("@inbUserId", SqlDbType.BigInt) { Value=UserId},
                };
                DataTable dataTable = dbconnect.SPExecuteDataTable("[WebApplication_SP].[usp_User_ProfileDetail_ById]", sqlparameters, "dataTable");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    userProfile.UserId = dataTable.Rows[0]["UserId"] == DBNull.Value ? "0" : Convert.ToString(dataTable.Rows[0]["UserId"]);
                    userProfile.UserName = dataTable.Rows[0]["UserName"] == DBNull.Value ? string.Empty : Convert.ToString(dataTable.Rows[0]["UserName"]);
                    userProfile.FullName = dataTable.Rows[0]["FullName"] == DBNull.Value ? string.Empty : Convert.ToString(dataTable.Rows[0]["FullName"]);
                    userProfile.EmailId = dataTable.Rows[0]["EmailId"] == DBNull.Value ? string.Empty : Convert.ToString(dataTable.Rows[0]["EmailId"]);
                    userProfile.MobileNumber = dataTable.Rows[0]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dataTable.Rows[0]["MobileNumber"]);
                    userProfile.ProfileImage = dataTable.Rows[0]["ProfileImage"] == DBNull.Value ? string.Empty : Convert.ToString(dataTable.Rows[0]["ProfileImage"]);
                }
                else
                {
                    userProfile = null;
                }
            }
            return userProfile;
        }
        #endregion
        
        public async Task<long> ChangePassword(string newPassword, string userId)
        {
            try
            {
                long result = 0;
                using (var dbConnect = connectionFactory.GetDAL)
                {
                    SqlParameter[] sqlparameters =
                    {
                    new SqlParameter("@intbUserId", SqlDbType.BigInt) { Value = userId },
                    new SqlParameter("@chvnNewPassword", SqlDbType.VarChar) { Value = newPassword },
                    new SqlParameter("@chvnOperationType",SqlDbType.VarChar){ Value = "CHANGEPASSWORD"}
                    };
                    result = await Task.Run(() => dbConnect.SPExecuteScalarReturnValue("[WebApplication_SP].[usp_user_Profile_ChangePassword]", sqlparameters));
                }
                return result;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return 0;
            }
        }
        public async Task<long> ChangeProfileImage(string imgSrcPath, string userId)
        {
            try
            {
                long result = 0;
                using (var dbConnect = connectionFactory.GetDAL)
                {
                    SqlParameter[] sqlparameters =
                    {
                    new SqlParameter("@intbUserId", SqlDbType.BigInt) { Value = userId },
                    new SqlParameter("@chvnImagePath", SqlDbType.VarChar) { Value = imgSrcPath },
                    new SqlParameter("@chvnOperationType",SqlDbType.VarChar){ Value = "CHANGEPROFILEIMAGE"}
                    };
                    result = await Task.Run(() => dbConnect.SPExecuteScalarReturnValue("[WebApplication_SP].[usp_User_ProfileImage]", sqlparameters));
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
