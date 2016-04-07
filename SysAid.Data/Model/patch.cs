namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("patch")]
    public partial class patch
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string patch_id { get; set; }

        [Required]
        [StringLength(255)]
        public string product_name { get; set; }

        [Required]
        [StringLength(255)]
        public string vendor { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string file_digest { get; set; }

        [StringLength(64)]
        public string bulletin { get; set; }

        [Column(TypeName = "ntext")]
        public string title { get; set; }

        public int? max_download_size { get; set; }

        public int? classification { get; set; }

        [StringLength(64)]
        public string severity_type { get; set; }

        public DateTime? release_date { get; set; }

        [StringLength(64)]
        public string kb_article_id { get; set; }

        [Column(TypeName = "ntext")]
        public string switches { get; set; }

        [Column(TypeName = "ntext")]
        public string url { get; set; }

        [Column(TypeName = "ntext")]
        public string file_url { get; set; }

        [Column(TypeName = "ntext")]
        public string language { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? uninstallable { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? security_update { get; set; }
    }
}
