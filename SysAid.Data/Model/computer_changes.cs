namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class computer_changes
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
        public DateTime change_time { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(64)]
        public string change_type { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(64)]
        public string change_sub_type { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "ntext")]
        public string change_description { get; set; }

        public int? log_id { get; set; }

        public int? policy_id { get; set; }
    }
}
