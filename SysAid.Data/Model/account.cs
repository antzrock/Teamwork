namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account")]
    public partial class account
    {
        [Key]
        [StringLength(32)]
        public string account_id { get; set; }

        [StringLength(64)]
        public string customer_name { get; set; }

        public DateTime? expiration_time { get; set; }

        [Required]
        [StringLength(64)]
        public string serial_key { get; set; }

        [Column(TypeName = "ntext")]
        public string account_conf { get; set; }

        public int version { get; set; }
    }
}
