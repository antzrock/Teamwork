namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class custom_services
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(64)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string service_name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string service_class { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int service_interval { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int service_start_time { get; set; }
    }
}
