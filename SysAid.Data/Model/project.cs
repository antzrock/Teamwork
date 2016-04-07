namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("project")]
    public partial class project
    {
        public int id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        public int version { get; set; }

        public int? category { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        [Column(TypeName = "ntext")]
        public string description { get; set; }

        public int? status { get; set; }

        [Column(TypeName = "ntext")]
        public string notes { get; set; }

        public DateTime? start_time { get; set; }

        public DateTime? end_time { get; set; }

        public int? raw_estimation { get; set; }

        [StringLength(64)]
        public string request_group { get; set; }

        [StringLength(64)]
        public string manager { get; set; }

        [StringLength(64)]
        public string assigned_group { get; set; }

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

        public int? company { get; set; }

        public int? company_backup { get; set; }

        [StringLength(255)]
        public string incidentTitle { get; set; }

        public DateTime? cust_date1 { get; set; }

        public DateTime? cust_date2 { get; set; }

        public int? progress { get; set; }
    }
}
