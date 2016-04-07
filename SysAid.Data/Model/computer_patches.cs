namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class computer_patches
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string patch_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(64)]
        public string computer_id { get; set; }

        public int? patch_status { get; set; }

        [StringLength(255)]
        public string failure_reason { get; set; }

        public int? change_id { get; set; }

        public DateTime? installed_date { get; set; }

        public int? manual_event_id { get; set; }

        public DateTime? start_schedule_date_time { get; set; }
    }
}
