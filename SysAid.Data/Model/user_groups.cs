namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user_groups
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string group_name { get; set; }

        public int group_type { get; set; }

        public int support_level { get; set; }

        [Column(TypeName = "ntext")]
        public string permission { get; set; }

        [StringLength(1)]
        public string display_group { get; set; }
    }
}
