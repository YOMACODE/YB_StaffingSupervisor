using System;
using System.Collections.Generic;
using System.Text;
using YB_StaffingSupervisor.DataAccess.Entities;

namespace YB_StaffingSupervisor.DataAccess.Contract
{
    public interface ILeftMenuRepository
    {
        LeftMenuModel GetLeftMenuList(Int64? userId);
    }
}
