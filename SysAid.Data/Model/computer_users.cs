namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class computer_users
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string computer_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(64)]
        public string user_name { get; set; }

        [StringLength(255)]
        public string full_name { get; set; }

        [StringLength(64)]
        public string email_address { get; set; }
    }
}
