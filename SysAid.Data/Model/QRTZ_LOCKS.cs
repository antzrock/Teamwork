namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QRTZ_LOCKS
    {
        [Key]
        [StringLength(40)]
        public string LOCK_NAME { get; set; }
    }
}
