namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class problem_type
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column("problem_type", Order = 1)]
        [StringLength(64)]
        public string problem_type1 { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(64)]
        public string problem_sub_type { get; set; }

        [StringLength(64)]
        public string route { get; set; }

        [Column(TypeName = "ntext")]
        public string desc_template { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(64)]
        public string third_level_category { get; set; }

        public int module_relevance { get; set; }

        public int? incident_template { get; set; }

        public int? request_template { get; set; }

        public int? change_template { get; set; }

        public int? problem_template { get; set; }
    }
}
