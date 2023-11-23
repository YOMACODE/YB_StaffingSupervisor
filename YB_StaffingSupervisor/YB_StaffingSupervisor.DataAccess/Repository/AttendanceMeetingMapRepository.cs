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
    public class AttendanceMeetingMapRepository : BaseRepository, IAttendanceMeetingMapRepository
    {
        public AttendanceMeetingMapRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<AttendanceMeetingMapCustom> GetAttendanceMeetingMapList(string AttendanceDate, string UserId)
        {
            AttendanceMeetingMapCustom result = new AttendanceMeetingMapCustom();
            try
            {
                using var dbconnect = connectionFactory.GetDAL;
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@intbUserId",SqlDbType.BigInt){ Value = UserId},
                    new SqlParameter("@chvnAttendanceDate",SqlDbType.VarChar){ Value = AttendanceDate},
                    new SqlParameter("@chvnOperationType",SqlDbType.NVarChar){Value="GetAttendanceMeetingMap"},
                };
                List<AttendanceMeetingMapModel> attendanceMeetingMapModels = new List<AttendanceMeetingMapModel>();
                DataSet dataSet = await Task.Run(() => dbconnect.SPExecuteDataset("[WebApplication_SP].[usp_Supervisor_GetAttendanceMeetingMap]", sqlParameters, "dataSet"));
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                        {
                            AttendanceMeetingMapModel attendanceMeetingMapModel = new AttendanceMeetingMapModel();
                            attendanceMeetingMapModel.SNo = dataRow["SNo"] == DBNull.Value ? string.Empty : Convert.ToString(dataRow["SNo"]);
                            attendanceMeetingMapModel.MeetingTitle = dataRow["MeetingTitle"] == DBNull.Value ? string.Empty : Convert.ToString(dataRow["MeetingTitle"]);
                            attendanceMeetingMapModel.MeetingDescription = dataRow["MeetingDescription"] == DBNull.Value ? string.Empty : Convert.ToString(dataRow["MeetingDescription"]);
                            attendanceMeetingMapModel.CheckInTime = dataRow["CheckInTime"] == DBNull.Value ? string.Empty : Convert.ToString(dataRow["CheckInTime"]);
                            attendanceMeetingMapModel.CheckOutTime = dataRow["CheckOutTime"] == DBNull.Value ? string.Empty : Convert.ToString(dataRow["CheckOutTime"]);
                            attendanceMeetingMapModel.DistanceTravel = dataRow["DistanceTravel"] == DBNull.Value ? string.Empty : Convert.ToString(dataRow["DistanceTravel"]);
                            attendanceMeetingMapModels.Add(attendanceMeetingMapModel);
                        }
                        result.attendanceMeetingMapListing = attendanceMeetingMapModels;
                    }
                    if (dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0)
                    {
                        result.TotalDistanceTravel = dataSet.Tables[1].Rows[0]["TotalDistanceTravel"] == DBNull.Value ? "0" : Convert.ToString(dataSet.Tables[1].Rows[0]["TotalDistanceTravel"]);
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return result;
        }
    }
}
