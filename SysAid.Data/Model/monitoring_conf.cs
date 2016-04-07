namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class monitoring_conf
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string computer_id { get; set; }

        [Required]
        [StringLength(1)]
        public string is_server { get; set; }

        [StringLength(64)]
        public string template_name { get; set; }

        [StringLength(64)]
        public string notification { get; set; }

        public int? alert { get; set; }

        public int? sr_sent { get; set; }

        public int? mail_sent { get; set; }

        public int? sms_sent { get; set; }

        [StringLength(1)]
        public string monitoring_enabled { get; set; }
    }
}
