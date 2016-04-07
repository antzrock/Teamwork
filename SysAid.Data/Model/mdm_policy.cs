namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class mdm_policy
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        public DateTime? request_time { get; set; }

        public int? revision { get; set; }

        [StringLength(1)]
        public string enable_password { get; set; }

        [StringLength(1)]
        public string allow_simple_password { get; set; }

        [StringLength(1)]
        public string alphanumeric_password_req { get; set; }

        public int? min_password_length { get; set; }

        public int? min_complex_password { get; set; }

        public int? max_password_age { get; set; }

        public int? auto_lock { get; set; }

        public int? password_history { get; set; }

        public int? max_failed_password { get; set; }

        public int? email_type { get; set; }

        [StringLength(255)]
        public string account_name { get; set; }

        [StringLength(255)]
        public string path_prefix_imap { get; set; }

        [StringLength(255)]
        public string user_display_name { get; set; }

        [StringLength(64)]
        public string email_address { get; set; }

        [StringLength(64)]
        public string host_name { get; set; }

        [StringLength(32)]
        public string server_port { get; set; }

        [StringLength(64)]
        public string user_name { get; set; }

        public int? auth_type { get; set; }

        [StringLength(1)]
        public string use_ssl { get; set; }

        [StringLength(64)]
        public string outoging_host_name { get; set; }

        [StringLength(32)]
        public string outgoing_server_port { get; set; }

        [StringLength(64)]
        public string outgoing_user_name { get; set; }

        public int? outgoing_auth_type { get; set; }

        [StringLength(1)]
        public string outgoing_use_ssl { get; set; }

        [StringLength(64)]
        public string domain_name { get; set; }

        public int? sync_emails_date_range { get; set; }

        public int? passcode_grace_period { get; set; }
    }
}
