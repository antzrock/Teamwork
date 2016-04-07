namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class news
    {
        [Key]
        public int news_id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        [StringLength(1)]
        public string present { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        [Column(TypeName = "ntext")]
        public string description { get; set; }

        [StringLength(1)]
        public string administrator { get; set; }

        [StringLength(1)]
        public string urgency { get; set; }

        public DateTime? insert_time { get; set; }

        public int? company { get; set; }

        public int? company_backup { get; set; }

        [StringLength(64)]
        public string assigned_group { get; set; }
    }
}
