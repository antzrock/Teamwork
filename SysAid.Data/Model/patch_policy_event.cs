namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class patch_policy_event
    {
        public int id { get; set; }

        public int policy_id { get; set; }

        [StringLength(64)]
        public string user_name { get; set; }

        public DateTime? event_time { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? is_scan { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? is_manual { get; set; }
    }
}
