namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sysaid_user_permissions
    {
        [Key]
        [StringLength(64)]
        public string user_name { get; set; }

        [Column(TypeName = "ntext")]
        public string permission_conf { get; set; }
    }
}
