namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class chat_closed_sessions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int session_id { get; set; }

        public int chat_status { get; set; }

        [StringLength(64)]
        public string request_user { get; set; }

        public int? service_request { get; set; }

        [Required]
        [StringLength(64)]
        public string queue { get; set; }

        [StringLength(64)]
        public string assigned_user { get; set; }

        [Required]
        [StringLength(64)]
        public string session_password { get; set; }

        [StringLength(255)]
        public string full_name { get; set; }

        [StringLength(64)]
        public string email_address { get; set; }

        [StringLength(64)]
        public string ip_address { get; set; }

        public DateTime? start_time { get; set; }

        public DateTime? close_time { get; set; }

        public DateTime? update_time { get; set; }

        public DateTime? accept_time { get; set; }

        [StringLength(64)]
        public string account_id { get; set; }

        public int? line_count { get; set; }

        [Column(TypeName = "ntext")]
        public string session_text { get; set; }
    }
}
