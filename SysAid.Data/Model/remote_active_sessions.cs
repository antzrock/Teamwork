namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class remote_active_sessions
    {
        [Required]
        [StringLength(64)]
        public string user_name { get; set; }

        [Required]
        [StringLength(64)]
        public string source_host { get; set; }

        [Required]
        [StringLength(64)]
        public string target_host { get; set; }

        [Key]
        [StringLength(64)]
        public string session_id { get; set; }

        [Required]
        [StringLength(64)]
        public string rcg { get; set; }

        public long? session_start_time { get; set; }
    }
}
