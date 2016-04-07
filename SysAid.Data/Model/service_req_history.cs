namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class service_req_history
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Required]
        [StringLength(64)]
        public string computer_id { get; set; }

        public int? ci_id { get; set; }

        [StringLength(64)]
        public string problem_type { get; set; }

        [StringLength(64)]
        public string problem_sub_type { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        [Column(TypeName = "ntext")]
        public string description { get; set; }

        [Column(TypeName = "ntext")]
        public string workaround { get; set; }

        [StringLength(1)]
        public string known_error { get; set; }

        public int? status { get; set; }

        [Column(TypeName = "ntext")]
        public string contact { get; set; }

        [StringLength(64)]
        public string responsibility { get; set; }

        public int? urgency { get; set; }

        public int? priority { get; set; }

        [Column(TypeName = "ntext")]
        public string notes { get; set; }

        [Column(TypeName = "ntext")]
        public string resolution { get; set; }

        [Column(TypeName = "ntext")]
        public string solution { get; set; }

        public DateTime? insert_time { get; set; }

        public DateTime? update_time { get; set; }

        public DateTime? close_time { get; set; }

        [StringLength(64)]
        public string update_user { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int version { get; set; }

        public int knowledge_base { get; set; }

        [StringLength(64)]
        public string submit_user { get; set; }

        public byte submit_user_type { get; set; }

        [StringLength(64)]
        public string request_user { get; set; }

        public byte request_user_type { get; set; }

        [StringLength(64)]
        public string email_account { get; set; }

        [StringLength(64)]
        public string responsible_manager { get; set; }

        public DateTime? due_date { get; set; }

        public int? location { get; set; }

        public int? parent_link { get; set; }

        public int? escalation { get; set; }

        [StringLength(64)]
        public string third_level_category { get; set; }

        [StringLength(64)]
        public string assigned_group { get; set; }

        public DateTime? timers_update_time { get; set; }

        public long? timer1 { get; set; }

        public long? timer2 { get; set; }

        public long? timer3 { get; set; }

        public long? timer4 { get; set; }

        public long? timer5 { get; set; }

        public long? timer6 { get; set; }

        public long? timer7 { get; set; }

        public long? timer8 { get; set; }

        public long? timer9 { get; set; }

        public long? timer10 { get; set; }

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

        [StringLength(255)]
        public string cc { get; set; }

        public int? project_id { get; set; }

        public int? task_id { get; set; }

        public int? sr_type { get; set; }

        [StringLength(255)]
        public string full_name { get; set; }

        public DateTime? cust_date1 { get; set; }

        public DateTime? cust_date2 { get; set; }

        public int? source { get; set; }

        public int? sr_sub_type { get; set; }

        public DateTime? followup_planned_date { get; set; }

        public DateTime? followup_actual_date { get; set; }

        [StringLength(64)]
        public string followup_user { get; set; }

        [Column(TypeName = "ntext")]
        public string followup_text { get; set; }

        public int? success_rating { get; set; }

        public int? reopen_counter { get; set; }

        public int? assign_counter { get; set; }

        public int? max_support_level { get; set; }

        public int? current_support_level { get; set; }

        public int? agreement { get; set; }

        public int? survey_status { get; set; }

        public int? impact { get; set; }

        public int? change_category { get; set; }

        public int? archive { get; set; }

        public int? closure_information { get; set; }

        [StringLength(1)]
        public string visible_to_eu { get; set; }

        [StringLength(255)]
        public string sr_class { get; set; }
    }
}
