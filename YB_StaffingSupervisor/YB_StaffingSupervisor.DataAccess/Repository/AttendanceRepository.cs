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
    public class AttendanceRepository : BaseRepository, IAttendanceRepository
    {
        public AttendanceRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public async Task<TeamAttendanceCustom> GetDailyTeamAttendanceListing(TeamAttendanceCustom SearchRequest)
        {
            TeamAttendanceCustom teamAttendanceCustom = new TeamAttendanceCustom();
            using (var dbconnect = connectionFactory.GetDAL)
            {
                SqlParameter[] sqlparameters =
                {
                    new SqlParameter("@intbSupervisorId",SqlDbType.BigInt){Value = SearchRequest.SupervisorId},
                    new SqlParameter("@chvnSearchAttendanceDate", SqlDbType.VarChar) { Value = SearchRequest.SearchDate},
                    new SqlParameter("@chvnSearchUserCode", SqlDbType.VarChar) { Value = SearchRequest.SearchUserCode},
                    new SqlParameter("@chvnOperationType", SqlDbType.VarChar) { Value = "TeamAttendance" },
                };
                List<AttendanceModel> quickAttendanceModels = new List<AttendanceModel>();
                List<AttendanceModel> selfieBasedAttendanceModels = new List<AttendanceModel>();
                DataSet dataSet = await Task.Run(() => dbconnect.SPExecuteDataset("[WebApplication_SP].[usp_Supervisor_AttendanceRequest_ApproveReject_SelectAll_SelectById]", sqlparameters, "dataSet"));
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {
                            AttendanceModel attendanceModel = new AttendanceModel();
                            attendanceModel.DailyAttendanceId = dataSet.Tables[0].Rows[i]["DailyAttendanceId"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["DailyAttendanceId"]);
                            attendanceModel.UserCode = dataSet.Tables[0].Rows[i]["UserCode"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["UserCode"]);
                            attendanceModel.FullName = dataSet.Tables[0].Rows[i]["FullName"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["FullName"]);
                            attendanceModel.AttendanceDate = dataSet.Tables[0].Rows[i]["AttendanceDate"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["AttendanceDate"]);
                            attendanceModel.AttendanceType = dataSet.Tables[0].Rows[i]["AttendanceType"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["AttendanceType"]);
                            attendanceModel.CheckInTime = dataSet.Tables[0].Rows[i]["CheckInTime"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInTime"]);
                            attendanceModel.CheckOutTime = dataSet.Tables[0].Rows[i]["CheckOutTime"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutTime"]);
                            attendanceModel.WorkingHours = dataSet.Tables[0].Rows[i]["CompleteHours"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CompleteHours"]);
                            attendanceModel.ApproveRejectStatus = dataSet.Tables[0].Rows[i]["ApproveRejectStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectStatus"]);
                            attendanceModel.ApproveRejectComment = dataSet.Tables[0].Rows[i]["ApproveRejectComment"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectComment"]);
                            attendanceModel.CheckInStatus = dataSet.Tables[0].Rows[i]["CheckInStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInStatus"]);
                            attendanceModel.CheckOutStatus = dataSet.Tables[0].Rows[i]["CheckOutStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutStatus"]);
                            attendanceModel.ShiftHours = dataSet.Tables[0].Rows[i]["ShiftHours"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ShiftHours"]);
                            attendanceModel.CheckInTimeFrom = dataSet.Tables[0].Rows[i]["CheckInTimeFrom"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInTimeFrom"]);
                            attendanceModel.CheckInTimeTo = dataSet.Tables[0].Rows[i]["CheckInTimeTo"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInTimeTo"]);
                            attendanceModel.CheckOutTimeFrom = dataSet.Tables[0].Rows[i]["CheckOutTimeFrom"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutTimeFrom"]);
                            attendanceModel.CheckOutTimeTo = dataSet.Tables[0].Rows[i]["CheckOutTimeTo"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutTimeTo"]);
                            attendanceModel.UserId = dataSet.Tables[0].Rows[i]["UserId"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["UserId"]);
                            attendanceModel.EmailId = dataSet.Tables[0].Rows[i]["EmailId"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["EmailId"]);
                            attendanceModel.MobileNumber = dataSet.Tables[0].Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["MobileNumber"]);
                            attendanceModel.CheckInLatitude = dataSet.Tables[0].Rows[i]["CheckInLatitude"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInLatitude"]);
                            attendanceModel.CheckInLongitude = dataSet.Tables[0].Rows[i]["CheckInLongitude"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInLongitude"]);
                            attendanceModel.CheckOutLatitude = dataSet.Tables[0].Rows[i]["CheckOutLatitude"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutLatitude"]);
                            attendanceModel.CheckOutLongitude = dataSet.Tables[0].Rows[i]["CheckOutLongitude"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutLongitude"]);
                            attendanceModel.CheckInImagePath = dataSet.Tables[0].Rows[i]["CheckInImagePath"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInImagePath"]);
                            attendanceModel.CheckOutImagePath = dataSet.Tables[0].Rows[i]["CheckOutImagePath"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutImagePath"]);
                            quickAttendanceModels.Add(attendanceModel);
                        }
                        teamAttendanceCustom.quickAttendanceListing = quickAttendanceModels;
                    }
                    if (dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                        {
                            AttendanceModel attendanceModel = new AttendanceModel();
                            attendanceModel.DailyAttendanceId = dataSet.Tables[1].Rows[i]["DailyAttendanceId"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[1].Rows[i]["DailyAttendanceId"]);
                            attendanceModel.UserCode = dataSet.Tables[1].Rows[i]["UserCode"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["UserCode"]);
                            attendanceModel.FullName = dataSet.Tables[1].Rows[i]["FullName"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["FullName"]);
                            attendanceModel.AttendanceDate = dataSet.Tables[1].Rows[i]["AttendanceDate"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["AttendanceDate"]);
                            attendanceModel.AttendanceType = dataSet.Tables[1].Rows[i]["AttendanceType"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["AttendanceType"]);
                            attendanceModel.CheckInTime = dataSet.Tables[1].Rows[i]["CheckInTime"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["CheckInTime"]);
                            attendanceModel.CheckOutTime = dataSet.Tables[1].Rows[i]["CheckOutTime"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["CheckOutTime"]);
                            attendanceModel.WorkingHours = dataSet.Tables[1].Rows[i]["CompleteHours"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["CompleteHours"]);
                            attendanceModel.ApproveRejectStatus = dataSet.Tables[1].Rows[i]["ApproveRejectStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["ApproveRejectStatus"]);
                            attendanceModel.ApproveRejectComment = dataSet.Tables[1].Rows[i]["ApproveRejectComment"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["ApproveRejectComment"]);
                            attendanceModel.CheckInStatus = dataSet.Tables[1].Rows[i]["CheckInStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["CheckInStatus"]);
                            attendanceModel.CheckOutStatus = dataSet.Tables[1].Rows[i]["CheckOutStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["CheckOutStatus"]);
                            attendanceModel.ShiftHours = dataSet.Tables[1].Rows[i]["ShiftHours"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["ShiftHours"]);
                            attendanceModel.CheckInTimeFrom = dataSet.Tables[1].Rows[i]["CheckInTimeFrom"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["CheckInTimeFrom"]);
                            attendanceModel.CheckInTimeTo = dataSet.Tables[1].Rows[i]["CheckInTimeTo"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["CheckInTimeTo"]);
                            attendanceModel.CheckOutTimeFrom = dataSet.Tables[1].Rows[i]["CheckOutTimeFrom"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["CheckOutTimeFrom"]);
                            attendanceModel.CheckOutTimeTo = dataSet.Tables[1].Rows[i]["CheckOutTimeTo"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["CheckOutTimeTo"]);
                            attendanceModel.UserId = dataSet.Tables[1].Rows[i]["UserId"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["UserId"]);
                            attendanceModel.EmailId = dataSet.Tables[1].Rows[i]["EmailId"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["EmailId"]);
                            attendanceModel.MobileNumber = dataSet.Tables[1].Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["MobileNumber"]);
                            attendanceModel.CheckInLatitude = dataSet.Tables[1].Rows[i]["CheckInLatitude"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["CheckInLatitude"]);
                            attendanceModel.CheckInLongitude = dataSet.Tables[1].Rows[i]["CheckInLongitude"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["CheckInLongitude"]);
                            attendanceModel.CheckOutLatitude = dataSet.Tables[1].Rows[i]["CheckOutLatitude"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["CheckOutLatitude"]);
                            attendanceModel.CheckOutLongitude = dataSet.Tables[1].Rows[i]["CheckOutLongitude"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["CheckOutLongitude"]);
                            attendanceModel.CheckInImagePath = dataSet.Tables[1].Rows[i]["CheckInImagePath"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["CheckInImagePath"]);
                            attendanceModel.CheckOutImagePath = dataSet.Tables[1].Rows[i]["CheckOutImagePath"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["CheckOutImagePath"]);
                            selfieBasedAttendanceModels.Add(attendanceModel);
                        }
                        teamAttendanceCustom.selfieBasedAttendanceListing = selfieBasedAttendanceModels;
                    }
                    if (dataSet.Tables[2] != null && dataSet.Tables[2].Rows.Count > 0)
                    {
                        teamAttendanceCustom.Date = dataSet.Tables[2].Rows[0]["Date"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[0]["Date"]);
                        teamAttendanceCustom.TotalAssociates = dataSet.Tables[2].Rows[0]["TotalAssociates"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[0]["TotalAssociates"]);
                        teamAttendanceCustom.TotalPresent = dataSet.Tables[2].Rows[0]["TotalPresent"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[0]["TotalPresent"]);
                        teamAttendanceCustom.TotalAbsent = dataSet.Tables[2].Rows[0]["TotalAbsent"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[0]["TotalAbsent"]);
                    }
                }
            }
            return teamAttendanceCustom;
        }
        public async Task<AttendanceRequestCustom> GetAttendanceRequestListing(int Page, int PageSize, AttendanceRequestCustom SearchRequest)
        {
            AttendanceRequestCustom attendanceRequestCustom = new AttendanceRequestCustom();
            using (var dbconnect = connectionFactory.GetDAL)
            {
                SqlParameter[] sqlparameters =
                {
                    new SqlParameter("@intbSupervisorId",SqlDbType.BigInt){Value = SearchRequest.SupervisorId},
                    new SqlParameter("@chvnSearchUserCode",SqlDbType.VarChar){Value = SearchRequest.SearchUserCode},
                    new SqlParameter("@chvnSearchFromDate",SqlDbType.VarChar){Value = SearchRequest.SearchAttendanceFrom},
                    new SqlParameter("@chvnSearchToDate",SqlDbType.VarChar){Value = SearchRequest.SearchAttendanceTo},
                    new SqlParameter("@chvnSearchStatusType",SqlDbType.VarChar){Value = SearchRequest.SearchStatusType},
                    new SqlParameter("@intOffsetValue",SqlDbType.Int){ Value=(Page-1) * PageSize },
                    new SqlParameter("@intPagingSize",SqlDbType.Int){ Value=PageSize },
                    new SqlParameter("@chvnSortOrderBy", SqlDbType.NVarChar,10) { Value = SearchRequest.SortOrderBy},
                    new SqlParameter("@chvnSortColumnName", SqlDbType.NVarChar,512) { Value = SearchRequest.SortColumnName},
                    new SqlParameter("@chvnOperationType", SqlDbType.VarChar) { Value = "SelectAll" },
                };
                List<AttendanceModel> attendanceModels = new List<AttendanceModel>();
                DataSet dataSet = await Task.Run(() => dbconnect.SPExecuteDataset("[WebApplication_SP].[usp_Supervisor_AttendanceRequest_ApproveReject_SelectAll_SelectById]", sqlparameters, "dataSet"));
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {
                            AttendanceModel attendanceModel = new AttendanceModel();
                            attendanceModel.DailyAttendanceId = dataSet.Tables[0].Rows[i]["DailyAttendanceId"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[0].Rows[i]["DailyAttendanceId"]);
                            attendanceModel.UserCode = dataSet.Tables[0].Rows[i]["UserCode"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["UserCode"]);
                            attendanceModel.FullName = dataSet.Tables[0].Rows[i]["FullName"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["FullName"]);
                            attendanceModel.AttendanceDate = dataSet.Tables[0].Rows[i]["AttendanceDate"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["AttendanceDate"]);
                            attendanceModel.AttendanceType = dataSet.Tables[0].Rows[i]["AttendanceType"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["AttendanceType"]);
                            attendanceModel.CheckInTime = dataSet.Tables[0].Rows[i]["CheckInTime"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInTime"]);
                            attendanceModel.CheckOutTime = dataSet.Tables[0].Rows[i]["CheckOutTime"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutTime"]);
                            attendanceModel.WorkingHours = dataSet.Tables[0].Rows[i]["CompleteHours"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CompleteHours"]);
                            attendanceModel.ApproveRejectStatus = dataSet.Tables[0].Rows[i]["ApproveRejectStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectStatus"]);
                            attendanceModel.ApproveRejectComment = dataSet.Tables[0].Rows[i]["ApproveRejectComment"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ApproveRejectComment"]);
                            attendanceModel.CheckInStatus = dataSet.Tables[0].Rows[i]["CheckInStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInStatus"]);
                            attendanceModel.CheckOutStatus = dataSet.Tables[0].Rows[i]["CheckOutStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutStatus"]);
                            attendanceModel.ShiftHours = dataSet.Tables[0].Rows[i]["ShiftHours"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["ShiftHours"]);
                            attendanceModel.CheckInTimeFrom = dataSet.Tables[0].Rows[i]["CheckInTimeFrom"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInTimeFrom"]);
                            attendanceModel.CheckInTimeTo = dataSet.Tables[0].Rows[i]["CheckInTimeTo"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInTimeTo"]);
                            attendanceModel.CheckOutTimeFrom = dataSet.Tables[0].Rows[i]["CheckOutTimeFrom"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutTimeFrom"]);
                            attendanceModel.CheckOutTimeTo = dataSet.Tables[0].Rows[i]["CheckOutTimeTo"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutTimeTo"]);
                            attendanceModel.UserId = dataSet.Tables[0].Rows[i]["UserId"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["UserId"]);
                            attendanceModel.EmailId = dataSet.Tables[0].Rows[i]["EmailId"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["EmailId"]);
                            attendanceModel.MobileNumber = dataSet.Tables[0].Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["MobileNumber"]);
                            attendanceModel.CheckInLatitude = dataSet.Tables[0].Rows[i]["CheckInLatitude"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInLatitude"]);
                            attendanceModel.CheckInLongitude = dataSet.Tables[0].Rows[i]["CheckInLongitude"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInLongitude"]);
                            attendanceModel.CheckOutLatitude = dataSet.Tables[0].Rows[i]["CheckOutLatitude"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutLatitude"]);
                            attendanceModel.CheckOutLongitude = dataSet.Tables[0].Rows[i]["CheckOutLongitude"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutLongitude"]);
                            attendanceModel.CheckInImagePath = dataSet.Tables[0].Rows[i]["CheckInImagePath"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckInImagePath"]);
                            attendanceModel.CheckOutImagePath = dataSet.Tables[0].Rows[i]["CheckOutImagePath"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[i]["CheckOutImagePath"]);
                            attendanceModels.Add(attendanceModel);
                        }
                    }
                    var pager = new CustomPagination((dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0 && dataSet.Tables[1].Columns.Contains("TotalRecords") == true) ? Convert.ToInt32(dataSet.Tables[1].Rows[0]["TotalRecords"]) : 0, Page, PageSize);
                    attendanceRequestCustom.attendanceListing = attendanceModels;
                    attendanceRequestCustom.CustomPagination = pager;
                }
            }
            return attendanceRequestCustom;
        }
        public async Task<UserAttendanceCustom> GetUserAttendanceListing(UserAttendanceCustom SearchRequest)
        {
            UserAttendanceCustom userAttendanceCustom = new UserAttendanceCustom();
            using (var dbconnect = connectionFactory.GetDAL)
            {
                SqlParameter[] sqlparameters =
                {
                    new SqlParameter("@intbSupervisorId",SqlDbType.BigInt){Value = SearchRequest.SupervisorId},
                    new SqlParameter("@intbUserId",SqlDbType.BigInt){Value = SearchRequest.UserId},
                    new SqlParameter("@intMonth", SqlDbType.Int) { Value = SearchRequest.SearchMonth},
                    new SqlParameter("@intYear", SqlDbType.Int) { Value = SearchRequest.SearchYear },
                    new SqlParameter("@chvnOperationType", SqlDbType.VarChar) { Value = "UserAttendance" },
                };
                List<AttendanceModel> attendanceModels = new List<AttendanceModel>();
                List<AttendanceCalendarModel> attendanceCalendarModels = new List<AttendanceCalendarModel>();
                DataSet dataSet = await Task.Run(() => dbconnect.SPExecuteDataset("[WebApplication_SP].[usp_Supervisor_AttendanceRequest_ApproveReject_SelectAll_SelectById]", sqlparameters, "dataSet"));
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        userAttendanceCustom.TotalWorkingDays = dataSet.Tables[0].Rows[0]["TotalWorkingDays"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[0]["TotalWorkingDays"]);
                        userAttendanceCustom.TotalPresents = dataSet.Tables[0].Rows[0]["TotalPresents"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[0]["TotalPresents"]);
                        userAttendanceCustom.TotalLeaves = dataSet.Tables[0].Rows[0]["TotalLeaves"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[0]["TotalLeaves"]);
                        userAttendanceCustom.TotalHolidays = dataSet.Tables[0].Rows[0]["TotalHolidays"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[0]["TotalHolidays"]);
                        userAttendanceCustom.TotalWeekoffs = dataSet.Tables[0].Rows[0]["TotalWeekoffs"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[0]["TotalWeekoffs"]);
                        userAttendanceCustom.TotalAbsents = dataSet.Tables[0].Rows[0]["TotalAbsents"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[0]["TotalAbsents"]);
                        userAttendanceCustom.TotalWorkingHours = dataSet.Tables[0].Rows[0]["TotalWorkingHours"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[0]["TotalWorkingHours"]);
                        userAttendanceCustom.FullName = dataSet.Tables[0].Rows[0]["FullName"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[0]["FullName"]);
                        userAttendanceCustom.UserCode = dataSet.Tables[0].Rows[0]["UserCode"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[0]["UserCode"]);
                        userAttendanceCustom.Month = dataSet.Tables[0].Rows[0]["Month"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[0]["Month"]);
                        userAttendanceCustom.Year = dataSet.Tables[0].Rows[0]["Year"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[0].Rows[0]["Year"]);
                    }
                    if (dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                        {
                            AttendanceCalendarModel attendanceCalendarModel = new AttendanceCalendarModel();
                            attendanceCalendarModel.SNo = dataSet.Tables[1].Rows[i]["RNo"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["RNo"]);
                            attendanceCalendarModel.Date = dataSet.Tables[1].Rows[i]["Date"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["Date"]);
                            attendanceCalendarModel.Day = dataSet.Tables[1].Rows[i]["Day"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["Day"]);
                            attendanceCalendarModel.DateDay = dataSet.Tables[1].Rows[i]["DateDay"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[1].Rows[i]["DateDay"]);
                            attendanceCalendarModels.Add(attendanceCalendarModel);
                        }
                        userAttendanceCustom.attendanceCalendarListing = attendanceCalendarModels;
                    }
                    if (dataSet.Tables[2] != null && dataSet.Tables[2].Rows.Count > 0)
                    {
                        for (int i = 0; i < dataSet.Tables[2].Rows.Count; i++)
                        {
                            AttendanceModel attendanceModel = new AttendanceModel();
                            attendanceModel.DailyAttendanceId = dataSet.Tables[2].Rows[i]["DailyAttendanceId"] == DBNull.Value ? Convert.ToString(0) : Convert.ToString(dataSet.Tables[2].Rows[i]["DailyAttendanceId"]);
                            attendanceModel.UserCode = dataSet.Tables[2].Rows[i]["UserCode"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["UserCode"]);
                            attendanceModel.FullName = dataSet.Tables[2].Rows[i]["FullName"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["FullName"]);
                            attendanceModel.AttendanceDate = dataSet.Tables[2].Rows[i]["AttendanceDate"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["AttendanceDate"]);
                            attendanceModel.AttendanceType = dataSet.Tables[2].Rows[i]["AttendanceType"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["AttendanceType"]);
                            attendanceModel.CheckInTime = dataSet.Tables[2].Rows[i]["CheckInTime"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["CheckInTime"]);
                            attendanceModel.CheckOutTime = dataSet.Tables[2].Rows[i]["CheckOutTime"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["CheckOutTime"]);
                            attendanceModel.WorkingHours = dataSet.Tables[2].Rows[i]["CompleteHours"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["CompleteHours"]);
                            attendanceModel.ApproveRejectStatus = dataSet.Tables[2].Rows[i]["ApproveRejectStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["ApproveRejectStatus"]);
                            attendanceModel.ApproveRejectComment = dataSet.Tables[2].Rows[i]["ApproveRejectComment"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["ApproveRejectComment"]);
                            attendanceModel.CheckInStatus = dataSet.Tables[2].Rows[i]["CheckInStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["CheckInStatus"]);
                            attendanceModel.CheckOutStatus = dataSet.Tables[2].Rows[i]["CheckOutStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["CheckOutStatus"]);
                            attendanceModel.ShiftHours = dataSet.Tables[2].Rows[i]["ShiftHours"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["ShiftHours"]);
                            attendanceModel.CheckInTimeFrom = dataSet.Tables[2].Rows[i]["CheckInTimeFrom"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["CheckInTimeFrom"]);
                            attendanceModel.CheckInTimeTo = dataSet.Tables[2].Rows[i]["CheckInTimeTo"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["CheckInTimeTo"]);
                            attendanceModel.CheckOutTimeFrom = dataSet.Tables[2].Rows[i]["CheckOutTimeFrom"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["CheckOutTimeFrom"]);
                            attendanceModel.CheckOutTimeTo = dataSet.Tables[2].Rows[i]["CheckOutTimeTo"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["CheckOutTimeTo"]);
                            attendanceModel.UserId = dataSet.Tables[2].Rows[i]["UserId"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["UserId"]);
                            attendanceModel.EmailId = dataSet.Tables[2].Rows[i]["EmailId"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["EmailId"]);
                            attendanceModel.MobileNumber = dataSet.Tables[2].Rows[i]["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["MobileNumber"]);
                            attendanceModel.CheckInLatitude = dataSet.Tables[2].Rows[i]["CheckInLatitude"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["CheckInLatitude"]);
                            attendanceModel.CheckInLongitude = dataSet.Tables[2].Rows[i]["CheckInLongitude"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["CheckInLongitude"]);
                            attendanceModel.CheckOutLatitude = dataSet.Tables[2].Rows[i]["CheckOutLatitude"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["CheckOutLatitude"]);
                            attendanceModel.CheckOutLongitude = dataSet.Tables[2].Rows[i]["CheckOutLongitude"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["CheckOutLongitude"]);
                            attendanceModel.CheckInImagePath = dataSet.Tables[2].Rows[i]["CheckInImagePath"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["CheckInImagePath"]);
                            attendanceModel.CheckOutImagePath = dataSet.Tables[2].Rows[i]["CheckOutImagePath"] == DBNull.Value ? string.Empty : Convert.ToString(dataSet.Tables[2].Rows[i]["CheckOutImagePath"]);
                            attendanceModels.Add(attendanceModel);
                        }
                        userAttendanceCustom.attendanceListing = attendanceModels;
                    }

                }
            }
            return userAttendanceCustom;
        }

        public async Task<long> AttendanceVerification(string attendanceId, string approveRejectstatus, string approveRejectComment, string approveRejectBy)
        {
            try
            {
                long result = 0;
                using (var dbConnect = connectionFactory.GetDAL)
                {
                    SqlParameter[] sqlparameters =
                    {
                    new SqlParameter("@intbDailyAttendanceId",SqlDbType.BigInt){ Value = attendanceId},
                    new SqlParameter("@chvnApproveRejectStatus", SqlDbType.NVarChar) { Value = approveRejectstatus },
                    new SqlParameter("@chvnApproveRejectComment", SqlDbType.NVarChar) { Value = approveRejectComment },
                    new SqlParameter("@intbApproveRejectBy",SqlDbType.BigInt){ Value = approveRejectBy},
                    new SqlParameter("@chvnOperationType",SqlDbType.NVarChar){ Value = "ApproveRejectAttendance"}
                    };
                    result = await Task.Run(() => dbConnect.SPExecuteScalarReturnValue("[WebApplication_SP].[usp_Supervisor_AttendanceRequest_ApproveReject_SelectAll_SelectById]", sqlparameters));
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
