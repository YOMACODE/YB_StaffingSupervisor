using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace YB_StaffingSupervisor.DataAccess.Entities.Model
{
	public class ClaimRequestsModel
	{
		//public string ClaimDate { get; set; }
		public string YomaId { get; set;}
		public string ClaimRequestId { get; set;}
		public string UserId { get; set;}
		public string AdditionalStatus { get; set;}
		public string SNo { get; set;}
		public string AssociateName { get; set;}
		public string Email { get; set;}
		public string MobileNumber { get; set;}
		public string ShowClaim { get; set;}
		public string ClaimInitiateDate { get; set;}
		public string DistanceTravelled { get; set;}
		public string DistanceCharge { get; set;}
		public string ViewRouteMap { get; set;}
		public string ClaimType { get; set;}
		public string ClaimAmount { get; set;}
		public string Remark { get; set;}
		public string ClaimSupportingImagePath { get; set;}
		public string ApproveRejectComment { get; set;}
		public string ApproveRejectStatus { get; set;}
		
	}
}
