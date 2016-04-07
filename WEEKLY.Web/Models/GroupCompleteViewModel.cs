using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class GroupCompleteViewModel
    {
        public int GroupID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public int UserID { get; set; }

        public GroupStatusViewModel Status { get; set; }

        public List<GroupMemberViewModel> Members { get; set; }

        public List<TeamViewModel> Teams { get; set; }

        public List<ProjectViewModel> Projects { get; set; }
    }
}