namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("computer")]
    public partial class computer
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string computer_id { get; set; }

        [StringLength(64)]
        public string computer_name { get; set; }

        [StringLength(64)]
        public string computer_type { get; set; }

        [Required]
        [StringLength(255)]
        public string parent_group { get; set; }

        [Column(TypeName = "ntext")]
        public string inventory_xml { get; set; }

        public DateTime? inventory_time { get; set; }

        public DateTime? update_time { get; set; }

        [StringLength(64)]
        public string ip_address { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [StringLength(64)]
        public string username { get; set; }

        [StringLength(255)]
        public string location { get; set; }

        public int? location_idx { get; set; }

        [StringLength(64)]
        public string building { get; set; }

        [StringLength(64)]
        public string floor { get; set; }

        [StringLength(64)]
        public string cubic { get; set; }

        [StringLength(64)]
        public string catalog_number { get; set; }

        public int? supplier { get; set; }

        public int? maintenance_supplier { get; set; }

        [StringLength(64)]
        public string company_serial { get; set; }

        [StringLength(64)]
        public string external_serial { get; set; }

        [StringLength(64)]
        public string monitor { get; set; }

        [StringLength(64)]
        public string monitor_serial { get; set; }

        public int? collection_type { get; set; }

        [Column(TypeName = "ntext")]
        public string collection_params { get; set; }

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

        [StringLength(64)]
        public string parent_asset { get; set; }

        public int? department { get; set; }

        public int? company { get; set; }

        public int? company_backup { get; set; }

        [StringLength(1)]
        public string disable { get; set; }

        [StringLength(1)]
        public string manual_asset { get; set; }

        public double? purchase_cost { get; set; }

        public int? purchase_currency { get; set; }

        [StringLength(64)]
        public string agent_version { get; set; }

        public DateTime? cust_date1 { get; set; }

        public DateTime? cust_date2 { get; set; }

        public DateTime? last_access { get; set; }

        public DateTime? last_remote_access { get; set; }

        [StringLength(64)]
        public string last_remote_host { get; set; }

        public int version { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_1 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_2 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_3 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_4 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_5 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_6 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_7 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_8 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_9 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_10 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_11 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_12 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_13 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_14 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_15 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_16 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_17 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_18 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_19 { get; set; }

        [StringLength(255)]
        public string snmp_cust_text_20 { get; set; }

        public double? packets_in { get; set; }

        public double? packets_out { get; set; }

        [StringLength(255)]
        public string mac_address { get; set; }

        public DateTime? last_boot { get; set; }

        public int? track_asset { get; set; }

        public DateTime? first_access { get; set; }

        public int? device_status { get; set; }

        public int? device_policy { get; set; }

        public int? device_ownership { get; set; }

        [StringLength(255)]
        public string device_imei { get; set; }

        [StringLength(255)]
        public string device_icc { get; set; }

        [StringLength(255)]
        public string device_home_carrier { get; set; }

        [StringLength(255)]
        public string device_current_carrier { get; set; }

        [StringLength(64)]
        public string device_phone_number { get; set; }

        [StringLength(255)]
        public string device_push { get; set; }

        [StringLength(255)]
        public string ios_push_magic { get; set; }

        [Column(TypeName = "ntext")]
        public string ios_unlock_token { get; set; }

        [StringLength(64)]
        public string designated_rds { get; set; }

        [StringLength(64)]
        public string gfi_version { get; set; }

        [StringLength(64)]
        public string gfi_build { get; set; }

        public int? policy_id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? patch_enabled { get; set; }
    }
}
