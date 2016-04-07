namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class generic_messages
    {
        [Key]
        public int msg_id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        [Column(TypeName = "ntext")]
        public string description { get; set; }

        [StringLength(1)]
        public string msg_type { get; set; }

        public DateTime? expiration_date { get; set; }

        public int? remaining_days { get; set; }

        [StringLength(1)]
        public string disable { get; set; }

        [Required]
        [StringLength(64)]
        public string identify { get; set; }
    }
}
