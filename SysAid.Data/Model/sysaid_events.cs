namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sysaid_events
    {
        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string description { get; set; }

        public DateTime? start_date { get; set; }

        public DateTime? end_date { get; set; }

        [StringLength(64)]
        public string available_groups { get; set; }

        [StringLength(64)]
        public string create_user { get; set; }
    }
}
