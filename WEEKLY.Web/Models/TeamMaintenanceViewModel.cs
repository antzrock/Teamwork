using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class TeamMaintenanceViewModel
    {
        public int TeamID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public GroupViewModel Group { get; set; }

        public TeamStatusViewModel Status { get; set; }
    }
}