namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class online_users_history
    {
        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

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

        public DateTime? login_time { get; set; }

        public DateTime? logout_time { get; set; }
    }
}
