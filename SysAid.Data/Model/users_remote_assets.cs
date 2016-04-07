namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class users_remote_assets
    {
        public int id { get; set; }

        [Required]
        [StringLength(64)]
        public string user_name { get; set; }

        [Required]
        [StringLength(64)]
        public string login_domain { get; set; }

        [Required]
        [StringLength(64)]
        public string login_user_upper { get; set; }

        [Required]
        [StringLength(64)]
        public string computer_id { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [StringLength(255)]
        public string note { get; set; }
    }
}
