﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class RoleViewModel
    {
        public int RoleID { get; set; }

        public string Name { get; set; }

        public bool isSystemGenerated { get; set; }

        public virtual ICollection<PermissionViewModel> DefaultPermissions { get; set; }

        public int[] Permissions { get; set; }
        
    }
}