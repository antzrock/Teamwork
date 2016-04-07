namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("supplier")]
    public partial class supplier
    {
        [Key]
        public int supplier_id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Column(TypeName = "ntext")]
        public string address { get; set; }

        [StringLength(64)]
        public string phone { get; set; }

        [StringLength(64)]
        public string fax { get; set; }

        [StringLength(64)]
        public string email_address { get; set; }

        [Column(TypeName = "ntext")]
        public string notes { get; set; }

        [StringLength(64)]
        public string mobile { get; set; }

        [StringLength(64)]
        public string phone2 { get; set; }

        [StringLength(64)]
        public string contact_name { get; set; }

        [StringLength(64)]
        public string account_number { get; set; }

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

        public int version { get; set; }
    }
}
