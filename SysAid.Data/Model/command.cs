namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class command
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column("command", Order = 1)]
        [StringLength(255)]
        public string command1 { get; set; }
    }
}
