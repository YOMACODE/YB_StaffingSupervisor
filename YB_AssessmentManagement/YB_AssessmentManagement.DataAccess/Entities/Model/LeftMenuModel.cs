using System.Collections.Generic;

namespace YB_AssessmentManagement.DataAccess.Entities
{
    public  class LeftMenuModel
    {
        public List<LeftParentMenu> leftParentMenus = new List<LeftParentMenu>();
        public string MenuList { get; set; }
    }
    public  class LeftParentMenu
    {
        public string ModuleName { get; set; }
        public bool IsSelected { get; set; }
        public string URL { get; set; }

        public List<LeftChildMenu> leftChildMenus = new List<LeftChildMenu>();
    }
    public  class LeftChildMenu
    {
        public string ModuleName { get; set; }
        public bool IsSelected { get; set; }
        public string URL { get; set; }
    }
}
