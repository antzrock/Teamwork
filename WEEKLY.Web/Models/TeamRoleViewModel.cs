﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class TeamRoleViewModel
    {
        public int TeamRoleID { get; set; }

        public string Name { get; set; }

        public bool isSystemGenerated { get; set; }

        public virtual ICollection<PermissionViewModel> DefaultPermissions { get; set; }

        public int[] Permissions { get; set; }
    }
}