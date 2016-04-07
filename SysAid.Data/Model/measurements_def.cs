namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class measurements_def
    {
        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        public int? agreement_id { get; set; }

        public int? parent_id { get; set; }

        public int? weight { get; set; }

        [StringLength(64)]
        public string formula1 { get; set; }

        public int? list1_id { get; set; }

        [StringLength(64)]
        public string formula2 { get; set; }

        public int? list2_id { get; set; }

        public int? time_interval { get; set; }

        [StringLength(64)]
        public string units { get; set; }

        public int? critical_grade { get; set; }

        public int? warning_grade { get; set; }

        public int? optimum_grade { get; set; }

        public int? goal_critical { get; set; }

        public int? goal_warning { get; set; }

        public int? goal_optimum { get; set; }

        public int? sla_critical { get; set; }

        public int? sla_warning { get; set; }

        public int? sla_optomum { get; set; }

        [StringLength(1)]
        public string calculated { get; set; }

        [StringLength(1)]
        public string enabled { get; set; }

        public int version { get; set; }
    }
}
