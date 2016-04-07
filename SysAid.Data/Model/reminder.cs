namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class reminder
    {
        public int id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        [Required]
        [StringLength(64)]
        public string reminder_name { get; set; }

        [Required]
        [StringLength(64)]
        public string notification { get; set; }

        public int alert_before { get; set; }

        [Required]
        [StringLength(64)]
        public string alert_time { get; set; }

        [Column(TypeName = "ntext")]
        public string filter_expression { get; set; }

        [Column(TypeName = "ntext")]
        public string filter_sql { get; set; }
    }
}
