namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class project_files
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

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

        [Column(TypeName = "image")]
        public byte[] file_content { get; set; }

        public DateTime? file_date { get; set; }

        [StringLength(255)]
        public string real_file_name { get; set; }
    }
}
