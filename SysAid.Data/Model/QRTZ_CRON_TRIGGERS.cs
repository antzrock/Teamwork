namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QRTZ_CRON_TRIGGERS
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(80)]
        public string TRIGGER_NAME { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(80)]
        public string TRIGGER_GROUP { get; set; }

        [Required]
        [StringLength(80)]
        public string CRON_EXPRESSION { get; set; }

        [StringLength(80)]
        public string TIME_ZONE_ID { get; set; }

        public virtual QRTZ_TRIGGERS QRTZ_TRIGGERS { get; set; }
    }
}
