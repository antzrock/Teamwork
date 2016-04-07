namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class monitor_embed_data
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

        [Key]
        [Column(Order = 2)]
        [StringLength(32)]
        public string check_type { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(200)]
        public string check_name { get; set; }

        [StringLength(64)]
        public string protocol { get; set; }

        public int? port_num { get; set; }

        [StringLength(255)]
        public string url_path { get; set; }

        [StringLength(255)]
        public string expression { get; set; }

        [StringLength(64)]
        public string update_type { get; set; }

        public int? warning_at { get; set; }

        [StringLength(64)]
        public string warning_notification { get; set; }

        public int? error_at { get; set; }

        [StringLength(64)]
        public string error_notification { get; set; }

        public int? idx_day { get; set; }

        public int? idx_week { get; set; }

        public int? idx_month { get; set; }

        public int? idx_year { get; set; }

        public DateTime? time_day { get; set; }

        public DateTime? time_week { get; set; }

        public DateTime? time_month { get; set; }

        public DateTime? time_year { get; set; }

        public int? warn_sr_sent { get; set; }

        public int? warn_mail_sent { get; set; }

        public int? warn_sms_sent { get; set; }

        public int? err_sr_sent { get; set; }

        public int? err_mail_sent { get; set; }

        public int? err_sms_sent { get; set; }

        public int? alert { get; set; }

        [StringLength(255)]
        public string extra_data { get; set; }

        public int? predefined_check { get; set; }
    }
}
