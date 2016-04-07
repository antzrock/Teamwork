namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class online_users
    {
        public int id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        [Required]
        [StringLength(64)]
        public string user_name { get; set; }

        [Required]
        [StringLength(64)]
        public string computer_id { get; set; }

        [StringLength(64)]
        public string computer_name { get; set; }

        [Required]
        [StringLength(64)]
        public string domain { get; set; }

        [StringLength(64)]
        public string client_name { get; set; }

        [StringLength(64)]
        public string ip_address { get; set; }

        [StringLength(64)]
        public string session_id { get; set; }

        public DateTime? last_update_date { get; set; }

        public int? disconnected { get; set; }
    }
}
