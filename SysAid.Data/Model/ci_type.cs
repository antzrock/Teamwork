namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ci_type
    {
        public int id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        [Required]
        [StringLength(64)]
        public string ci_type_name { get; set; }

        [Column(TypeName = "ntext")]
        public string description { get; set; }

        [StringLength(1)]
        public string predefined { get; set; }
    }
}
