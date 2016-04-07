using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class TeamCompleteViewModel
    {
        public int TeamID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public int UserID { get; set; }

        public GroupViewModel Group { get; set; }

        public TeamStatusViewModel Status { get; set; }

        public List<TeamMemberViewModel> Members { get; set; }

        public List<ProjectViewModel> Projects { get; set; }
    }
}