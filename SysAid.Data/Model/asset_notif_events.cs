namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class asset_notif_events
    {
        public int id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        [Required]
        [StringLength(64)]
        public string event_name { get; set; }

        [Required]
        [StringLength(64)]
        public string notification { get; set; }

        [Column(TypeName = "ntext")]
        public string filter_expression { get; set; }

        [Column(TypeName = "ntext")]
        public string filter_sql { get; set; }
    }
}
