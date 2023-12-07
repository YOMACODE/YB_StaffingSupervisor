using System;
using System.Collections.Generic;
using System.Text;

namespace YB_StaffingSupervisor.DataAccess.Entities.Model
{
	public class TeamMemberModel
	{
		public string UserCode { get; set; }
		public string FullName { get; set; }
		public string EmailId { get; set; }
		public string MobileNumber { get; set; }
		public string Designation { get; set; }
		public string JoiningDate { get; set; }
	}
}
