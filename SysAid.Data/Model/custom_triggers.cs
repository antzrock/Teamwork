namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class custom_triggers
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string entity_type { get; set; }

        [Column(TypeName = "ntext")]
        public string trigger_onload { get; set; }

        [Column(TypeName = "ntext")]
        public string trigger_before_save { get; set; }

        [Column(TypeName = "ntext")]
        public string trigger_after_save { get; set; }

        [Column(TypeName = "ntext")]
        public string trigger_onload_lastlog { get; set; }

        [Column(TypeName = "ntext")]
        public string trigger_before_save_lastlog { get; set; }

        [Column(TypeName = "ntext")]
        public string trigger_after_save_lastlog { get; set; }

        [StringLength(1)]
        public string compatibility_mode { get; set; }
    }
}
