using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;
using YB_StaffingSupervisor.DataAccess.Entities.Model;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
	public interface IMyTeamRepository
	{
		Task<TeamMemberCustom> GetTeamMembersListing(int Page, int PageSize, TeamMemberCustom SearchRequest);
		Task<DataTable> ExportTeamMemberList(string SearchUserCode, string SearchFullName, string SearchMobileNumber, string SearchEmailId, string SearchDesignation, string SearchJoiningDate);
	}
}
