namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class current_sla_results
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int measurement_id { get; set; }

        public double? current_result { get; set; }

        public double? sla_grade { get; set; }

        public double? internal_grade { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime run_date { get; set; }

        [StringLength(1)]
        public string final_result { get; set; }
    }
}
