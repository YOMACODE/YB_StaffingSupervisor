using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Entities.Custom;
using YB_StaffingSupervisor.DataAccess.Entities.Model;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
	public interface IMyTeamRepository
	{
		Task<TeamMemberCustom> GetTeamMembersListing(int Page, int PageSize, TeamMemberCustom SearchRequest);
	}
}
