namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sysaid_user_push_enable
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(64)]
        public string user_name { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Required]
        [StringLength(64)]
        public string device_type { get; set; }

        [Required]
        [StringLength(255)]
        public string device_id { get; set; }

        public DateTime? enable_date { get; set; }

        [StringLength(1)]
        public string is_production { get; set; }

        public int? is_chat_online { get; set; }
    }
}
