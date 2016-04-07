namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class discovery_service
    {
        public int id { get; set; }

        [Required]
        [StringLength(64)]
        public string discovery_service_name { get; set; }

        public DateTime? create_date { get; set; }

        public DateTime? upgrade_date { get; set; }

        public DateTime? last_connection_date { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [StringLength(64)]
        public string ip_address { get; set; }

        [StringLength(64)]
        public string location { get; set; }

        [Required]
        [StringLength(64)]
        public string version { get; set; }

        [StringLength(64)]
        public string owner { get; set; }

        [StringLength(255)]
        public string domain { get; set; }

        [StringLength(1)]
        public string windows { get; set; }

        [StringLength(255)]
        public string rds_url { get; set; }

        [StringLength(64)]
        public string client_version { get; set; }

        [StringLength(64)]
        public string gfi_version { get; set; }

        [StringLength(64)]
        public string gfi_build { get; set; }

        [StringLength(255)]
        public string pgfi_url { get; set; }
    }
}
