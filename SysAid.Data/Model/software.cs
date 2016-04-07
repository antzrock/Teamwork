namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("software")]
    public partial class software
    {
        [Key]
        public int software_id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        [Required]
        [StringLength(255)]
        public string product_name { get; set; }

        [StringLength(64)]
        public string version { get; set; }

        [StringLength(64)]
        public string vendor { get; set; }

        public int? licenses { get; set; }

        public DateTime? purchase_date { get; set; }

        public DateTime? support_expiration { get; set; }

        [Column(TypeName = "ntext")]
        public string notes { get; set; }

        public int? company { get; set; }

        public int? company_backup { get; set; }

        public int? supplier { get; set; }

        public int? cust_list1 { get; set; }

        public int? cust_list2 { get; set; }

        [StringLength(255)]
        public string cust_text1 { get; set; }

        [StringLength(255)]
        public string cust_text2 { get; set; }

        [Column(TypeName = "ntext")]
        public string cust_notes { get; set; }

        public int? cust_int1 { get; set; }

        public int? cust_int2 { get; set; }

        public DateTime? cust_date1 { get; set; }

        public DateTime? cust_date2 { get; set; }

        public int history_version { get; set; }

        [StringLength(1)]
        public string freeware { get; set; }

        public int? exceedlic_installed { get; set; }
    }
}
