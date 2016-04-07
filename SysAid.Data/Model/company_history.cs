namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class company_history
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int company_id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        [StringLength(255)]
        public string company_name { get; set; }

        [StringLength(255)]
        public string address { get; set; }

        [StringLength(255)]
        public string address2 { get; set; }

        [StringLength(64)]
        public string city { get; set; }

        [StringLength(64)]
        public string state { get; set; }

        [StringLength(64)]
        public string zip { get; set; }

        [StringLength(64)]
        public string country { get; set; }

        [StringLength(64)]
        public string phone { get; set; }

        [StringLength(64)]
        public string fax { get; set; }

        [Column(TypeName = "ntext")]
        public string notes { get; set; }

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

        public DateTime? expiration_time { get; set; }

        public DateTime? cust_date1 { get; set; }

        public DateTime? cust_date2 { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int version { get; set; }

        public DateTime? change_time { get; set; }

        [StringLength(64)]
        public string changed_by { get; set; }

        public int? agreement { get; set; }

        public DateTime? agreement_start { get; set; }

        public DateTime? agreement_end { get; set; }

        [StringLength(64)]
        public string logo_file_name { get; set; }
    }
}
