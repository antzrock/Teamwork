using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class UpdateTeamRoleViewModel
    {
        public int UserID { get; set; }

        public List<TeamRoleViewModel> TeamRoles { get; set; }
    }
}