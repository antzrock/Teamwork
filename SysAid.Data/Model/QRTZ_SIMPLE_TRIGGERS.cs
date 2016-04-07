namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QRTZ_SIMPLE_TRIGGERS
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(80)]
        public string TRIGGER_NAME { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(80)]
        public string TRIGGER_GROUP { get; set; }

        public long REPEAT_COUNT { get; set; }

        public long REPEAT_INTERVAL { get; set; }

        public long TIMES_TRIGGERED { get; set; }

        public virtual QRTZ_TRIGGERS QRTZ_TRIGGERS { get; set; }
    }
}
