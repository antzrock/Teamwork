namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class asset_types
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string computer_type { get; set; }

        [Required]
        [StringLength(64)]
        public string caption { get; set; }

        [StringLength(255)]
        public string file_name { get; set; }

        [StringLength(255)]
        public string default_file_name { get; set; }

        public int? ci_sub_type_id { get; set; }
    }
}
