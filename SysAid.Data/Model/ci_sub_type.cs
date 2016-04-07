namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ci_sub_type
    {
        public int id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        public int ci_type_id { get; set; }

        [Required]
        [StringLength(64)]
        public string caption { get; set; }

        [StringLength(255)]
        public string file_name { get; set; }
    }
}
