namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QRTZ_JOB_LISTENERS
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(80)]
        public string JOB_NAME { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(80)]
        public string JOB_GROUP { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(80)]
        public string JOB_LISTENER { get; set; }

        public virtual QRTZ_JOB_DETAILS QRTZ_JOB_DETAILS { get; set; }
    }
}
