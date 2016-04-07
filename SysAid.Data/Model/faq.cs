namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("faq")]
    public partial class faq
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        [Column(TypeName = "ntext")]
        public string question { get; set; }

        [Column(TypeName = "ntext")]
        public string answer { get; set; }

        [StringLength(64)]
        public string category { get; set; }

        [StringLength(64)]
        public string sub_category { get; set; }

        [StringLength(64)]
        public string third_level_category { get; set; }

        [StringLength(1)]
        public string kb { get; set; }

        [Column("faq")]
        [StringLength(1)]
        public string faq1 { get; set; }

        public DateTime? update_time { get; set; }

        public int admin_topic_views { get; set; }

        public int user_topic_views { get; set; }

        public DateTime? created_on { get; set; }

        [StringLength(64)]
        public string created_by { get; set; }

        [StringLength(64)]
        public string update_by { get; set; }

        public int? enable_expire { get; set; }

        public DateTime? expire_date { get; set; }

        [StringLength(255)]
        public string keywords { get; set; }

        public int? publish { get; set; }

        public int? voteYes { get; set; }

        public int? voteNo { get; set; }
    }
}
