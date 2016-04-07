using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class UserPermissionsViewModel
    {
        public List<PermissionViewModel> ApplicationPermissions { get; set; }

        public List<GroupPermissionViewModel> GroupPermissions { get; set; }

        public List<TeamPermissionViewModel> TeamPermissions { get; set; }

        public List<ProjectPermissionViewModel> ProjectPermissions { get; set; }

        public bool isUserAdmin { get; set; }
    }
}