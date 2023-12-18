using System;
using System.Collections.Generic;
using System.Text;

namespace YB_StaffingSupervisor.DataAccess.Entities.Model
{
    public class UserClaimRequestsModel
    {
        public string SNo{ get; set; }     
        public string Date{ get; set; }     
        public string DistanceTravelled{ get; set; }     
        public string DistanceCharge{ get; set; }     
        public string ViewRouteMap{ get; set; }     
        public string ClaimType{ get; set; }     
        public string ClaimAmount{ get; set; }     
        public string Description{ get; set; }     
        public string ViewSupporting{ get; set; }     
        public string Comment{ get; set; }     
        public string ClaimStatus{ get; set; }     
    }
}
