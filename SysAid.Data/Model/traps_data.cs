namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class traps_data
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string computer_id { get; set; }

        public int trap_0 { get; set; }

        public int trap_1 { get; set; }

        public int trap_2 { get; set; }

        public int trap_3 { get; set; }

        public int trap_4 { get; set; }

        public int trap_5 { get; set; }

        public int trap_6 { get; set; }
    }
}
