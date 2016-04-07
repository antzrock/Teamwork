namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class statistics_data
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string data_date { get; set; }

        public int? current_month_data_1 { get; set; }

        public int? current_month_data_2 { get; set; }

        public int? last_month_data_1 { get; set; }

        public int? last_month_data_2 { get; set; }

        public int? current_year_data_1 { get; set; }

        public int? current_year_data_2 { get; set; }

        public int? last_year_data_1 { get; set; }

        public int? last_year_data_2 { get; set; }
    }
}
