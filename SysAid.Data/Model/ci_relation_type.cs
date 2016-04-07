namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ci_relation_type
    {
        public int id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        [Required]
        [StringLength(64)]
        public string relation_name { get; set; }

        [StringLength(64)]
        public string opposite_relation_name { get; set; }

        [StringLength(1)]
        public string predefined { get; set; }
    }
}
