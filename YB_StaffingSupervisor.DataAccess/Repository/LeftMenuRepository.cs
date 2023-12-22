using System;
using System.Data;
using System.Data.SqlClient;
using YB_StaffingSupervisor.DataAccess.Contract;
using YB_StaffingSupervisor.DataAccess.Entities;
using YB_StaffingSupervisor.DataAccess.Infrastructure;

namespace YB_StaffingSupervisor.DataAccess.Repository
{
    public class LeftMenuRepository :BaseRepository , ILeftMenuRepository
    {
        public LeftMenuRepository(IConnectionFactory connectionFactory) : base(connectionFactory) 
        { 

        }

        #region Bind left menu based on UserId
        public LeftMenuModel GetLeftMenuList(Int64? UserId)
        {
            LeftMenuModel leftMenuModel = new LeftMenuModel();
            using (var dbconnect = connectionFactory.GetDAL)
            {
                SqlParameter[] sqlparameters =
                {
                    new SqlParameter("@inbUserId", SqlDbType.BigInt) { Value = UserId},
                };
                DataTable dataTable = dbconnect.SPExecuteDataTable("[WebApplication_SP].[usp_User_SideParentMenu_ByUserId]", sqlparameters, "dt");

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    LeftParentMenu leftParentMenu = new LeftParentMenu();
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        if (dataRow["ModuleName"] != DBNull.Value)
                        {
                            using (var dbconnect1 = connectionFactory.GetDAL)
                            {
                                leftParentMenu.ModuleName = dataRow["ModuleName"].ToString();
                                int ParentModuleId = Convert.ToInt32(dataRow["ModuleId"].ToString());
                                SqlParameter[] sqlParametersParent =
                                {
                                    new SqlParameter ("@inParentModuleId",SqlDbType.Int){ Value=ParentModuleId},
                                    new SqlParameter("@inbUserId", SqlDbType.BigInt) { Value = UserId},
                                };

                                DataTable dataTable1 = dbconnect1.SPExecuteDataTable("[WebApplication_SP].[usp_User_SideSubMenu_ByUserId]", sqlParametersParent, "dtchild");
                                if (dataTable1 != null && dataTable1.Rows.Count > 0)
                                {
                                    foreach (DataRow dataRow1 in dataTable1.Rows)
                                    {
                                        LeftChildMenu leftChildMenu = new LeftChildMenu();
                                        leftChildMenu.ModuleName = dataRow1["ModuleName"].ToString();
                                        leftChildMenu.URL = dataRow1["Url"].ToString();
                                        leftParentMenu.leftChildMenus.Add(leftChildMenu);
                                    }
                                }
                                else
                                {
                                    leftParentMenu.URL = dataRow["Url"] == DBNull.Value ? "" : dataRow["Url"].ToString();
                                }
                                leftMenuModel.leftParentMenus.Add(leftParentMenu);
                            }
                        }
                    }

                }
            }
            return leftMenuModel;
        }
        #endregion
    }
}
