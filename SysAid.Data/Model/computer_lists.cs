namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class computer_lists
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
        public string list_type { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(255)]
        public string value { get; set; }

        [StringLength(255)]
        public string list_display { get; set; }

        [StringLength(255)]
        public string version { get; set; }

        [StringLength(255)]
        public string license { get; set; }
    }
}
