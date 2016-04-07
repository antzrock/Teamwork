using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class ProjectPermissionViewModel
    {
        public int ProjectID { get; set; }

        public string ProjectName { get; set; }

        public ICollection<PermissionViewModel> Permissions { get; set; }
    }
}