namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class list_view
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string list_name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(64)]
        public string list_view_name { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(64)]
        public string user_name { get; set; }

        [Column(TypeName = "ntext")]
        public string list_conf { get; set; }

        [StringLength(1)]
        public string enable_delete { get; set; }

        public int? version { get; set; }
    }
}
