namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_sub_tab_history
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sr_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(64)]
        public string tab_name { get; set; }

        public int sub_tab_id { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sub_tab_order { get; set; }

        [StringLength(64)]
        public string assigned_to { get; set; }

        [StringLength(64)]
        public string submit_user { get; set; }

        public DateTime? insert_time { get; set; }

        public DateTime? due_date { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        [Column(TypeName = "ntext")]
        public string description { get; set; }

        [Column(TypeName = "ntext")]
        public string notes { get; set; }

        public DateTime? proposed_delivery_date { get; set; }

        [StringLength(64)]
        public string proposed_version { get; set; }

        public int? complexity { get; set; }

        public int? severity { get; set; }

        public int? urgency { get; set; }

        public int? priority { get; set; }

        public int? status { get; set; }

        public int? active { get; set; }

        [StringLength(1)]
        public string auto_complete { get; set; }

        public int? on_complete_change_status { get; set; }

        [StringLength(255)]
        public string notification_id { get; set; }

        [StringLength(64)]
        public string notification_method { get; set; }

        public double? duration { get; set; }

        public double? resources_required_in_days { get; set; }

        public int? ci_id { get; set; }

        [StringLength(64)]
        public string cab_meeting_reference { get; set; }

        public int? percent_completed { get; set; }

        public int? cust_int1 { get; set; }

        public int? cust_int2 { get; set; }

        public int? cust_int3 { get; set; }

        public int? cust_int4 { get; set; }

        public int? cust_int5 { get; set; }

        public int? cust_int6 { get; set; }

        public int? cust_int7 { get; set; }

        public int? cust_int8 { get; set; }

        public int? cust_int9 { get; set; }

        public int? cust_int10 { get; set; }

        [StringLength(255)]
        public string cust_text1 { get; set; }

        [StringLength(255)]
        public string cust_text2 { get; set; }

        [StringLength(255)]
        public string cust_text3 { get; set; }

        [StringLength(255)]
        public string cust_text4 { get; set; }

        [StringLength(255)]
        public string cust_text5 { get; set; }

        [StringLength(255)]
        public string cust_text6 { get; set; }

        [StringLength(255)]
        public string cust_text7 { get; set; }

        [StringLength(255)]
        public string cust_text8 { get; set; }

        [StringLength(255)]
        public string cust_text9 { get; set; }

        [StringLength(255)]
        public string cust_text10 { get; set; }

        public DateTime? cust_date1 { get; set; }

        public DateTime? cust_date2 { get; set; }

        public DateTime? cust_date3 { get; set; }

        public DateTime? cust_date4 { get; set; }

        public DateTime? cust_date5 { get; set; }

        public DateTime? cust_date6 { get; set; }

        public DateTime? cust_date7 { get; set; }

        public DateTime? cust_date8 { get; set; }

        public DateTime? cust_date9 { get; set; }

        public DateTime? cust_date10 { get; set; }

        public int? cust_list1 { get; set; }

        public int? cust_list2 { get; set; }

        public int? cust_list3 { get; set; }

        public int? cust_list4 { get; set; }

        public int? cust_list5 { get; set; }

        public int? cust_list6 { get; set; }

        public int? cust_list7 { get; set; }

        public int? cust_list8 { get; set; }

        public int? cust_list9 { get; set; }

        public int? cust_list10 { get; set; }

        [Column(TypeName = "ntext")]
        public string cust_notes1 { get; set; }

        [Column(TypeName = "ntext")]
        public string cust_notes2 { get; set; }

        [Column(TypeName = "ntext")]
        public string cust_notes3 { get; set; }

        [Column(TypeName = "ntext")]
        public string cust_notes4 { get; set; }

        [Column(TypeName = "ntext")]
        public string cust_notes5 { get; set; }

        [Column(TypeName = "ntext")]
        public string cust_notes6 { get; set; }

        [Column(TypeName = "ntext")]
        public string cust_notes7 { get; set; }

        [Column(TypeName = "ntext")]
        public string cust_notes8 { get; set; }

        [Column(TypeName = "ntext")]
        public string cust_notes9 { get; set; }

        [Column(TypeName = "ntext")]
        public string cust_notes10 { get; set; }

        public int? task_id { get; set; }

        public int? project_id { get; set; }

        [StringLength(64)]
        public string assigned_group { get; set; }

        public int? location { get; set; }

        public DateTime? completed_time { get; set; }

        public DateTime? enabled_time { get; set; }

        public int? company { get; set; }

        public int? company_backup { get; set; }

        public DateTime? modify_time { get; set; }

        [StringLength(1)]
        public string policy_compliance { get; set; }

        [StringLength(1)]
        public string budgeted { get; set; }

        [StringLength(1)]
        public string approved { get; set; }

        [StringLength(1)]
        public string user_acceptance { get; set; }

        public double? hardware_costs { get; set; }

        public double? software_costs { get; set; }

        public double? installation_costs { get; set; }

        public double? training_costs { get; set; }

        public double? maintenance_costs { get; set; }

        public double? total_costs { get; set; }

        public double? internal_manpower_in_hours { get; set; }

        public double? total_expected_downtime_hours { get; set; }

        public int? department { get; set; }

        public DateTime? expected_downtime_start { get; set; }

        public DateTime? expected_downtime_end { get; set; }

        public int? impact { get; set; }

        [StringLength(255)]
        public string reopened { get; set; }

        public double? cust_float1 { get; set; }

        public double? cust_float2 { get; set; }

        public double? cust_float3 { get; set; }

        public double? cust_float4 { get; set; }

        public double? cust_float5 { get; set; }

        public double? cust_float6 { get; set; }

        public double? cust_float7 { get; set; }

        public double? cust_float8 { get; set; }

        public double? cust_float9 { get; set; }

        public double? cust_float10 { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int version { get; set; }

        public DateTime? change_time { get; set; }

        [StringLength(64)]
        public string changed_by { get; set; }

        [StringLength(1)]
        public string allow_pdf { get; set; }

        [StringLength(64)]
        public string update_user { get; set; }

        [Column(TypeName = "ntext")]
        public string reopened_note { get; set; }

        [StringLength(1)]
        public string and_condition { get; set; }

        [StringLength(64)]
        public string additional_user { get; set; }

        public int? on_activate_change_status { get; set; }

        public int? on_completion_approve_patch { get; set; }
    }
}
