namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QRTZ_BLOB_TRIGGERS
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(80)]
        public string TRIGGER_NAME { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(80)]
        public string TRIGGER_GROUP { get; set; }

        [Column(TypeName = "image")]
        public byte[] BLOB_DATA { get; set; }
    }
}
