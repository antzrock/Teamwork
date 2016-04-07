namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class url_embed_data
    {
        [Key]
        [StringLength(200)]
        public string check_name { get; set; }

        [StringLength(255)]
        public string url_path { get; set; }

        [StringLength(255)]
        public string expression { get; set; }

        public int? retries { get; set; }

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

        public int? err_sr_sent { get; set; }

        public int? err_mail_sent { get; set; }

        public int? err_sms_sent { get; set; }

        public int? alert { get; set; }

        [StringLength(255)]
        public string extra_data { get; set; }
    }
}
