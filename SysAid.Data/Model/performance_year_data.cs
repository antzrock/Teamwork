namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class performance_year_data
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string computer_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(64)]
        public string check_name { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idx { get; set; }

        public double? check_value { get; set; }

        public DateTime? upd_time { get; set; }
    }
}
