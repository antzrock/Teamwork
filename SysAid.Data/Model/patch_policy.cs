namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class patch_policy
    {
        [Key]
        public int policy_id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        [StringLength(255)]
        public string policy_name { get; set; }

        public DateTime? policy_date { get; set; }

        public DateTime? last_scan { get; set; }

        public DateTime? next_scan { get; set; }

        public DateTime? last_patch { get; set; }

        public DateTime? next_patch { get; set; }

        [Column(TypeName = "ntext")]
        public string scan_schedule { get; set; }

        [Column(TypeName = "ntext")]
        public string patch_schedule { get; set; }

        [Column(TypeName = "ntext")]
        public string reboot_settings { get; set; }

        public DateTime? last_scan_server_time { get; set; }

        public DateTime? last_patch_server_time { get; set; }
    }
}
