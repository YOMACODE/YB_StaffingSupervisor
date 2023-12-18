using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using YB_StaffingSupervisor.DataAccess.Common;
using YB_StaffingSupervisor.DataAccess.Entities.Model;

namespace YB_StaffingSupervisor.DataAccess.Entities.Custom
{
    public class UserClaimRequetsCustom
    {
        #region Search and Sorting Parameter
        public string SearchClaimType { get; set; }
        public string SearchClaimStatus { get; set; }
        public string SearchDate { get; set; }
        public string SearchYear { get; set; }
        public string SearchMonth { get; set; }

        public string SortOrderBy { get; set; }
        public string SortColumnName { get; set; }
        public int? PageSize { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }

        //	public string LeaveRequestId { get; set; }
        #endregion
        public IEnumerable<UserClaimRequestsModel> UserClaimRequestModels { get; set; }
        public IEnumerable<UserClaimRequestsModel> UserClaimRequestListing { get; set; }
        #region Pagination
        public CustomPagination CustomPagination { get; set; }
        #endregion
        public SelectList monthModelsListing { get; set; }
        public SelectList yearModelsListing { get; set; }
    }
}
