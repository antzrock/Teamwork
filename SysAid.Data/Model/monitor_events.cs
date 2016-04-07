namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class monitor_events
    {
        public int id { get; set; }

        public int severity { get; set; }

        [StringLength(255)]
        public string source { get; set; }

        [StringLength(255)]
        public string source_name { get; set; }

        [StringLength(64)]
        public string monitor_type { get; set; }

        [StringLength(255)]
        public string monitor_target { get; set; }

        public int? port_num { get; set; }

        [StringLength(255)]
        public string url_path { get; set; }

        [StringLength(64)]
        public string category { get; set; }

        public DateTime? upd_time { get; set; }

        public double? check_value { get; set; }

        [StringLength(255)]
        public string expression { get; set; }

        public int? warning_threshold { get; set; }

        [StringLength(64)]
        public string warning_notification { get; set; }

        public int? error_threshold { get; set; }

        [StringLength(64)]
        public string error_notification { get; set; }

        public double? check_value1 { get; set; }

        public double? check_value2 { get; set; }

        [StringLength(64)]
        public string port_name { get; set; }
    }
}
