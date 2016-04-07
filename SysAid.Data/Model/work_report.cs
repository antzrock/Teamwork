namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class work_report
    {
        public int id { get; set; }

        public int? service_req_id { get; set; }

        [StringLength(32)]
        public string account_id { get; set; }

        [StringLength(64)]
        public string user_name { get; set; }

        public DateTime? from_time { get; set; }

        public DateTime? to_time { get; set; }

        [Column(TypeName = "ntext")]
        public string description { get; set; }

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

        public int? cust_int3 { get; set; }

        public int? cust_int4 { get; set; }

        public DateTime? cust_date1 { get; set; }

        public DateTime? cust_date2 { get; set; }

        public int? ci_id { get; set; }
    }
}
