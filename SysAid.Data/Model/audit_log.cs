namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class audit_log
    {
        public int id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        public DateTime? audit_date { get; set; }

        public int? audit_module { get; set; }

        public int? audit_sub_module { get; set; }

        public int? audit_type { get; set; }

        public int? audit_sub_type { get; set; }

        public int? audit_severity { get; set; }

        [Required]
        [StringLength(64)]
        public string user_name { get; set; }

        [StringLength(4000)]
        public string audit_info { get; set; }

        public int? max_line_id { get; set; }
    }
}
