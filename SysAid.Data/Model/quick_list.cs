namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class quick_list
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sr_id { get; set; }

        [StringLength(255)]
        public string quick_name { get; set; }

        [StringLength(255)]
        public string quick_class { get; set; }

        [StringLength(1)]
        public string quick_visible { get; set; }
    }
}
