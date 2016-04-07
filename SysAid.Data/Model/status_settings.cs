namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class status_settings
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        public int change_status { get; set; }

        public int incident_status { get; set; }

        [StringLength(255)]
        public string exclude_statuses { get; set; }
    }
}
