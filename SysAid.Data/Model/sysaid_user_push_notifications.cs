namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sysaid_user_push_notifications
    {
        public int? push_id { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(64)]
        public string user_name { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(32)]
        public string account_id { get; set; }

        public int? sr_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string push_message { get; set; }

        public int? status { get; set; }

        public DateTime? push_date { get; set; }
    }
}
