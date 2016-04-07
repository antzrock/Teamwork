namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class schedule_task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int task_id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        [Required]
        [StringLength(64)]
        public string computer_id { get; set; }

        [Required]
        [StringLength(255)]
        public string group_name { get; set; }

        [Required]
        [StringLength(255)]
        public string command { get; set; }

        public DateTime next_run { get; set; }

        public int granularity { get; set; }

        public int units { get; set; }

        [StringLength(64)]
        public string discovery_service_name { get; set; }

        [StringLength(64)]
        public string start_ip_range { get; set; }

        [StringLength(64)]
        public string end_ip_range { get; set; }

        [StringLength(255)]
        public string exclude_ip_list { get; set; }

        [StringLength(1)]
        public string schedule { get; set; }

        public int? snmp_version { get; set; }

        [StringLength(64)]
        public string community_string { get; set; }

        [StringLength(64)]
        public string community_password { get; set; }

        [StringLength(255)]
        public string domain { get; set; }

        [Column(TypeName = "ntext")]
        public string agent_settings { get; set; }

        [Column(TypeName = "ntext")]
        public string filter_sql { get; set; }

        [Column(TypeName = "ntext")]
        public string filter_xml { get; set; }
    }
}
