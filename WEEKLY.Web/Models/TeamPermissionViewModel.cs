using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class TeamPermissionViewModel
    {
        public int TeamID { get; set; }

        public string TeamName { get; set; }

        public ICollection<PermissionViewModel> Permissions { get; set; }
    }
}