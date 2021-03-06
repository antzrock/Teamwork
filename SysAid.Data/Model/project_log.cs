namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class project_log
    {
        [Key]
        public int log_id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        public int project_id { get; set; }

        public DateTime log_time { get; set; }

        [Required]
        [StringLength(64)]
        public string log_type { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string log_description { get; set; }

        public int ext_reference { get; set; }

        [Required]
        [StringLength(64)]
        public string user_name { get; set; }
    }
}
