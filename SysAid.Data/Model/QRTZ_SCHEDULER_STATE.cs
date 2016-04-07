namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QRTZ_SCHEDULER_STATE
    {
        [Key]
        [StringLength(80)]
        public string INSTANCE_NAME { get; set; }

        public long LAST_CHECKIN_TIME { get; set; }

        public long CHECKIN_INTERVAL { get; set; }

        [StringLength(80)]
        public string RECOVERER { get; set; }
    }
}
