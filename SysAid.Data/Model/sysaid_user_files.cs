namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sysaid_user_files
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(64)]
        public string id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(64)]
        public string file_id { get; set; }

        [StringLength(255)]
        public string file_name { get; set; }

        public DateTime? file_date { get; set; }

        [Column(TypeName = "image")]
        public byte[] file_content { get; set; }

        [StringLength(255)]
        public string real_file_name { get; set; }
    }
}
