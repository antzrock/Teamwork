namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QRTZ_TRIGGERS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QRTZ_TRIGGERS()
        {
            QRTZ_TRIGGER_LISTENERS = new HashSet<QRTZ_TRIGGER_LISTENERS>();
        }

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
        public string JOB_NAME { get; set; }

        [Required]
        [StringLength(80)]
        public string JOB_GROUP { get; set; }

        [Required]
        [StringLength(1)]
        public string IS_VOLATILE { get; set; }

        [StringLength(120)]
        public string DESCRIPTION { get; set; }

        public long? NEXT_FIRE_TIME { get; set; }

        public long? PREV_FIRE_TIME { get; set; }

        [Required]
        [StringLength(16)]
        public string TRIGGER_STATE { get; set; }

        [Required]
        [StringLength(8)]
        public string TRIGGER_TYPE { get; set; }

        public long START_TIME { get; set; }

        public long? END_TIME { get; set; }

        [StringLength(80)]
        public string CALENDAR_NAME { get; set; }

        public short? MISFIRE_INSTR { get; set; }

        [Column(TypeName = "image")]
        public byte[] JOB_DATA { get; set; }

        public virtual QRTZ_CRON_TRIGGERS QRTZ_CRON_TRIGGERS { get; set; }

        public virtual QRTZ_JOB_DETAILS QRTZ_JOB_DETAILS { get; set; }

        public virtual QRTZ_SIMPLE_TRIGGERS QRTZ_SIMPLE_TRIGGERS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QRTZ_TRIGGER_LISTENERS> QRTZ_TRIGGER_LISTENERS { get; set; }
    }
}
