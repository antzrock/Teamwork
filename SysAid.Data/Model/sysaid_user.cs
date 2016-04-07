namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sysaid_user
    {
        [Key]
        [StringLength(64)]
        public string user_name { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        [Required]
        [StringLength(64)]
        public string password { get; set; }

        [StringLength(64)]
        public string first_name { get; set; }

        [StringLength(64)]
        public string last_name { get; set; }

        [StringLength(1)]
        public string main_user { get; set; }

        [StringLength(64)]
        public string email_address { get; set; }

        [StringLength(255)]
        public string sms_number { get; set; }

        [StringLength(64)]
        public string ip_address { get; set; }

        [Column(TypeName = "ntext")]
        public string user_conf { get; set; }

        [StringLength(64)]
        public string phone { get; set; }

        [StringLength(64)]
        public string cell_phone { get; set; }

        [StringLength(255)]
        public string notes { get; set; }

        public int? location { get; set; }

        [StringLength(64)]
        public string car_number { get; set; }

        [StringLength(64)]
        public string building { get; set; }

        [StringLength(64)]
        public string floor { get; set; }

        [StringLength(64)]
        public string cubic { get; set; }

        [StringLength(1)]
        public string administrator { get; set; }

        [StringLength(1)]
        public string manager { get; set; }

        public int? version { get; set; }

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

        public int? department { get; set; }

        public int? company { get; set; }

        public int? company_backup { get; set; }

        [StringLength(1)]
        public string disable { get; set; }

        public DateTime? expiration_time { get; set; }

        public DateTime? cust_date1 { get; set; }

        public DateTime? cust_date2 { get; set; }

        public int history_version { get; set; }

        public int ldap { get; set; }

        public int? check_ldap { get; set; }

        [StringLength(1)]
        public string email_notifications { get; set; }

        [StringLength(1)]
        public string permissions_by_groups { get; set; }

        [StringLength(64)]
        public string user_manager_name { get; set; }

        [StringLength(64)]
        public string chat_nick_name { get; set; }

        [StringLength(1)]
        public string enable_login_to_eup { get; set; }

        public int? agreement { get; set; }

        [StringLength(64)]
        public string display_name { get; set; }

        [StringLength(64)]
        public string secondary_email { get; set; }

        [StringLength(64)]
        public string my_photo_url { get; set; }

        [Column(TypeName = "ntext")]
        public string sr_email_notif_condition { get; set; }

        [StringLength(64)]
        public string login_user { get; set; }

        [StringLength(64)]
        public string login_domain { get; set; }

        [StringLength(64)]
        public string login_guid { get; set; }

        [StringLength(255)]
        public string calculated_user_name { get; set; }

        [StringLength(255)]
        public string calculated_user_name_upper { get; set; }

        [StringLength(64)]
        public string locale { get; set; }

        [StringLength(64)]
        public string timezone { get; set; }

        [StringLength(64)]
        public string charset { get; set; }

        [StringLength(64)]
        public string login_user_upper { get; set; }

        [StringLength(64)]
        public string user_name_upper { get; set; }
    }
}
