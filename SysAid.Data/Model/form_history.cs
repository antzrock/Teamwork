namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class form_history
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(64)]
        public string user_name { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string form_caption { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(255)]
        public string form_url { get; set; }

        public DateTime? form_visit_time { get; set; }
    }
}
