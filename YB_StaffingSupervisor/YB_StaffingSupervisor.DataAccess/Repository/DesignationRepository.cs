using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Contract;
using YB_StaffingSupervisor.DataAccess.Entities;
using YB_StaffingSupervisor.DataAccess.Infrastructure;

namespace YB_StaffingSupervisor.DataAccess.Repository
{
    public class DesignationRepository:BaseRepository, IDesignationRepository
    {
        public DesignationRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
   
        /// <summary>
        /// Bind designation dropdown list
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DesignationModel>> DropdownDesignationList()
        {
            List<DesignationModel> designationModels = new List<DesignationModel>();
            using (var dbconnect = connectionFactory.GetDAL)
            {
                SqlParameter[] sqlParameters =
                {
                   new SqlParameter("@chvnOperationType", SqlDbType.NVarChar) { Value = "SELECTALLDESIGNATION" },
               };
                DataTable dataTable = await Task.Run(() => dbconnect.SPExecuteDataTable("[WebApplication_SP].[usp_Designation_Insert_Update_Delete_SelectAll_SelectById]", sqlParameters, "dataTable"));
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        designationModels.Add(new DesignationModel
                        {
                            DesignationName = Convert.ToString(dataTable.Rows[i]["DesignationName"]),
                            DesignationId = Convert.ToString(dataTable.Rows[i]["DesignationId"])
                        });
                    }
                    return designationModels;
                }
            }
            return designationModels;
        }
    }
}
