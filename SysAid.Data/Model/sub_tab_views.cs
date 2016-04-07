namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sub_tab_views
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public int id { get; set; }

        [StringLength(64)]
        public string name { get; set; }

        [Column(TypeName = "ntext")]
        public string sub_tab_view { get; set; }
    }
}
