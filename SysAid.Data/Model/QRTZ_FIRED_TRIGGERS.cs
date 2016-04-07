namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QRTZ_FIRED_TRIGGERS
    {
        [Key]
        [StringLength(95)]
        public string ENTRY_ID { get; set; }

        [Required]
        [StringLength(80)]
        public string TRIGGER_NAME { get; set; }

        [Required]
        [StringLength(80)]
        public string TRIGGER_GROUP { get; set; }

        [Required]
        [StringLength(1)]
        public string IS_VOLATILE { get; set; }

        [Required]
        [StringLength(80)]
        public string INSTANCE_NAME { get; set; }

        public long FIRED_TIME { get; set; }

        [Required]
        [StringLength(16)]
        public string STATE { get; set; }

        [StringLength(80)]
        public string JOB_NAME { get; set; }

        [StringLength(80)]
        public string JOB_GROUP { get; set; }

        [StringLength(1)]
        public string IS_STATEFUL { get; set; }

        [StringLength(1)]
        public string REQUESTS_RECOVERY { get; set; }
    }
}
