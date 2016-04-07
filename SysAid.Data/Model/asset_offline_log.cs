namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class asset_offline_log
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(64)]
        public string asset_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime offline_start_time { get; set; }

        public DateTime? offline_end_time { get; set; }

        public int? offline_minutes { get; set; }
    }
}
