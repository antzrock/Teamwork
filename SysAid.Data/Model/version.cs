namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("version")]
    public partial class version
    {
        [Key]
        [Column("version")]
        [StringLength(64)]
        public string version1 { get; set; }
    }
}
