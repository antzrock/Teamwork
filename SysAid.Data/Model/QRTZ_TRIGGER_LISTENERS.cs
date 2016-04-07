namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QRTZ_TRIGGER_LISTENERS
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(80)]
        public string TRIGGER_NAME { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(80)]
        public string TRIGGER_GROUP { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(80)]
        public string TRIGGER_LISTENER { get; set; }

        public virtual QRTZ_TRIGGERS QRTZ_TRIGGERS { get; set; }
    }
}
