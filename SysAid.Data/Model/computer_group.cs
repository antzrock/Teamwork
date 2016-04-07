namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class computer_group
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string group_name { get; set; }

        [Required]
        [StringLength(255)]
        public string parent_group_name { get; set; }

        [StringLength(255)]
        public string group_description { get; set; }

        public int? group_level { get; set; }
    }
}
