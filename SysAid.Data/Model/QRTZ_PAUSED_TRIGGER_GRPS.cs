namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QRTZ_PAUSED_TRIGGER_GRPS
    {
        [Key]
        [StringLength(80)]
        public string TRIGGER_GROUP { get; set; }
    }
}
