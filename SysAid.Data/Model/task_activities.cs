namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class task_activities
    {
        public int id { get; set; }

        public int? task_id { get; set; }

        [StringLength(32)]
        public string account_id { get; set; }

        [StringLength(64)]
        public string user_name { get; set; }

        public DateTime? from_time { get; set; }

        public DateTime? to_time { get; set; }

        [StringLength(4000)]
        public string description { get; set; }

        public int? activity_status { get; set; }

        public int? ci_id { get; set; }
    }
}
