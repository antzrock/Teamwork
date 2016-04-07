namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QRTZ_CALENDARS
    {
        [Key]
        [StringLength(80)]
        public string CALENDAR_NAME { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] CALENDAR { get; set; }
    }
}
