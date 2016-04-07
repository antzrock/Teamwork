namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class mdm_wifi_policy
    {
        public int id { get; set; }

        public int policy_id { get; set; }

        public int? type { get; set; }

        [StringLength(255)]
        public string ssid { get; set; }

        [StringLength(1)]
        public string auto_join { get; set; }

        [StringLength(1)]
        public string hidden_network { get; set; }

        [StringLength(64)]
        public string encryption_key { get; set; }

        public int? revision { get; set; }
    }
}
