using Microsoft.AspNetCore.Mvc;

namespace YB_StaffingSupervisor.Areas.MyTeam.Controllers
{
    [Area("MyTeam")]
    public class LeaveApprovalsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
