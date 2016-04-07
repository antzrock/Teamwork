namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ci_history
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        [Required]
        [StringLength(64)]
        public string ci_name { get; set; }

        [StringLength(64)]
        public string serial { get; set; }

        public int? ci_type { get; set; }

        public int? location { get; set; }

        [StringLength(64)]
        public string owner { get; set; }

        [StringLength(64)]
        public string owner_group { get; set; }

        public int? company { get; set; }

        public int? company_backup { get; set; }

        public int? supplier { get; set; }

        public DateTime? supply_date { get; set; }

        public DateTime? accept_date { get; set; }

        public int? status { get; set; }

        public int? priority { get; set; }

        [Column(TypeName = "ntext")]
        public string notes { get; set; }

        [StringLength(255)]
        public string import_desc { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int history_version { get; set; }

        [StringLength(64)]
        public string ci_cust_text_1 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_2 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_3 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_4 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_5 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_6 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_7 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_8 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_9 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_10 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_11 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_12 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_13 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_14 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_15 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_16 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_17 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_18 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_19 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_20 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_21 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_22 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_23 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_24 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_25 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_26 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_27 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_28 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_29 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_30 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_31 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_32 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_33 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_34 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_35 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_36 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_37 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_38 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_39 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_40 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_41 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_42 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_43 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_44 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_45 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_46 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_47 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_48 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_49 { get; set; }

        [StringLength(64)]
        public string ci_cust_text_50 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_1 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_2 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_3 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_4 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_5 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_6 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_7 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_8 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_9 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_10 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_11 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_12 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_13 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_14 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_15 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_16 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_17 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_18 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_19 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_20 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_21 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_22 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_23 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_24 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_25 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_26 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_27 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_28 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_29 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_30 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_31 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_32 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_33 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_34 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_35 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_36 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_37 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_38 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_39 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_40 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_41 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_42 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_43 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_44 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_45 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_46 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_47 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_48 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_49 { get; set; }

        [Column(TypeName = "ntext")]
        public string ci_cust_long_text_50 { get; set; }

        public DateTime? ci_cust_date_1 { get; set; }

        public DateTime? ci_cust_date_2 { get; set; }

        public DateTime? ci_cust_date_3 { get; set; }

        public DateTime? ci_cust_date_4 { get; set; }

        public DateTime? ci_cust_date_5 { get; set; }

        public DateTime? ci_cust_date_6 { get; set; }

        public DateTime? ci_cust_date_7 { get; set; }

        public DateTime? ci_cust_date_8 { get; set; }

        public DateTime? ci_cust_date_9 { get; set; }

        public DateTime? ci_cust_date_10 { get; set; }

        public DateTime? ci_cust_date_11 { get; set; }

        public DateTime? ci_cust_date_12 { get; set; }

        public DateTime? ci_cust_date_13 { get; set; }

        public DateTime? ci_cust_date_14 { get; set; }

        public DateTime? ci_cust_date_15 { get; set; }

        public DateTime? ci_cust_date_16 { get; set; }

        public DateTime? ci_cust_date_17 { get; set; }

        public DateTime? ci_cust_date_18 { get; set; }

        public DateTime? ci_cust_date_19 { get; set; }

        public DateTime? ci_cust_date_20 { get; set; }

        public DateTime? ci_cust_date_21 { get; set; }

        public DateTime? ci_cust_date_22 { get; set; }

        public DateTime? ci_cust_date_23 { get; set; }

        public DateTime? ci_cust_date_24 { get; set; }

        public DateTime? ci_cust_date_25 { get; set; }

        public DateTime? ci_cust_date_26 { get; set; }

        public DateTime? ci_cust_date_27 { get; set; }

        public DateTime? ci_cust_date_28 { get; set; }

        public DateTime? ci_cust_date_29 { get; set; }

        public DateTime? ci_cust_date_30 { get; set; }

        public DateTime? ci_cust_date_31 { get; set; }

        public DateTime? ci_cust_date_32 { get; set; }

        public DateTime? ci_cust_date_33 { get; set; }

        public DateTime? ci_cust_date_34 { get; set; }

        public DateTime? ci_cust_date_35 { get; set; }

        public DateTime? ci_cust_date_36 { get; set; }

        public DateTime? ci_cust_date_37 { get; set; }

        public DateTime? ci_cust_date_38 { get; set; }

        public DateTime? ci_cust_date_39 { get; set; }

        public DateTime? ci_cust_date_40 { get; set; }

        public DateTime? ci_cust_date_41 { get; set; }

        public DateTime? ci_cust_date_42 { get; set; }

        public DateTime? ci_cust_date_43 { get; set; }

        public DateTime? ci_cust_date_44 { get; set; }

        public DateTime? ci_cust_date_45 { get; set; }

        public DateTime? ci_cust_date_46 { get; set; }

        public DateTime? ci_cust_date_47 { get; set; }

        public DateTime? ci_cust_date_48 { get; set; }

        public DateTime? ci_cust_date_49 { get; set; }

        public DateTime? ci_cust_date_50 { get; set; }

        public int? ci_cust_list_1 { get; set; }

        public int? ci_cust_list_2 { get; set; }

        public int? ci_cust_list_3 { get; set; }

        public int? ci_cust_list_4 { get; set; }

        public int? ci_cust_list_5 { get; set; }

        public int? ci_cust_list_6 { get; set; }

        public int? ci_cust_list_7 { get; set; }

        public int? ci_cust_list_8 { get; set; }

        public int? ci_cust_list_9 { get; set; }

        public int? ci_cust_list_10 { get; set; }

        public int? ci_cust_list_11 { get; set; }

        public int? ci_cust_list_12 { get; set; }

        public int? ci_cust_list_13 { get; set; }

        public int? ci_cust_list_14 { get; set; }

        public int? ci_cust_list_15 { get; set; }

        public int? ci_cust_list_16 { get; set; }

        public int? ci_cust_list_17 { get; set; }

        public int? ci_cust_list_18 { get; set; }

        public int? ci_cust_list_19 { get; set; }

        public int? ci_cust_list_20 { get; set; }

        public int? ci_cust_list_21 { get; set; }

        public int? ci_cust_list_22 { get; set; }

        public int? ci_cust_list_23 { get; set; }

        public int? ci_cust_list_24 { get; set; }

        public int? ci_cust_list_25 { get; set; }

        public int? ci_cust_list_26 { get; set; }

        public int? ci_cust_list_27 { get; set; }

        public int? ci_cust_list_28 { get; set; }

        public int? ci_cust_list_29 { get; set; }

        public int? ci_cust_list_30 { get; set; }

        public int? ci_cust_list_31 { get; set; }

        public int? ci_cust_list_32 { get; set; }

        public int? ci_cust_list_33 { get; set; }

        public int? ci_cust_list_34 { get; set; }

        public int? ci_cust_list_35 { get; set; }

        public int? ci_cust_list_36 { get; set; }

        public int? ci_cust_list_37 { get; set; }

        public int? ci_cust_list_38 { get; set; }

        public int? ci_cust_list_39 { get; set; }

        public int? ci_cust_list_40 { get; set; }

        public int? ci_cust_list_41 { get; set; }

        public int? ci_cust_list_42 { get; set; }

        public int? ci_cust_list_43 { get; set; }

        public int? ci_cust_list_44 { get; set; }

        public int? ci_cust_list_45 { get; set; }

        public int? ci_cust_list_46 { get; set; }

        public int? ci_cust_list_47 { get; set; }

        public int? ci_cust_list_48 { get; set; }

        public int? ci_cust_list_49 { get; set; }

        public int? ci_cust_list_50 { get; set; }

        public int? ci_cust_int_1 { get; set; }

        public int? ci_cust_int_2 { get; set; }

        public int? ci_cust_int_3 { get; set; }

        public int? ci_cust_int_4 { get; set; }

        public int? ci_cust_int_5 { get; set; }

        public int? ci_cust_int_6 { get; set; }

        public int? ci_cust_int_7 { get; set; }

        public int? ci_cust_int_8 { get; set; }

        public int? ci_cust_int_9 { get; set; }

        public int? ci_cust_int_10 { get; set; }

        public int? ci_cust_int_11 { get; set; }

        public int? ci_cust_int_12 { get; set; }

        public int? ci_cust_int_13 { get; set; }

        public int? ci_cust_int_14 { get; set; }

        public int? ci_cust_int_15 { get; set; }

        public int? ci_cust_int_16 { get; set; }

        public int? ci_cust_int_17 { get; set; }

        public int? ci_cust_int_18 { get; set; }

        public int? ci_cust_int_19 { get; set; }

        public int? ci_cust_int_20 { get; set; }

        public int? ci_cust_int_21 { get; set; }

        public int? ci_cust_int_22 { get; set; }

        public int? ci_cust_int_23 { get; set; }

        public int? ci_cust_int_24 { get; set; }

        public int? ci_cust_int_25 { get; set; }

        public int? ci_cust_int_26 { get; set; }

        public int? ci_cust_int_27 { get; set; }

        public int? ci_cust_int_28 { get; set; }

        public int? ci_cust_int_29 { get; set; }

        public int? ci_cust_int_30 { get; set; }

        public int? ci_cust_int_31 { get; set; }

        public int? ci_cust_int_32 { get; set; }

        public int? ci_cust_int_33 { get; set; }

        public int? ci_cust_int_34 { get; set; }

        public int? ci_cust_int_35 { get; set; }

        public int? ci_cust_int_36 { get; set; }

        public int? ci_cust_int_37 { get; set; }

        public int? ci_cust_int_38 { get; set; }

        public int? ci_cust_int_39 { get; set; }

        public int? ci_cust_int_40 { get; set; }

        public int? ci_cust_int_41 { get; set; }

        public int? ci_cust_int_42 { get; set; }

        public int? ci_cust_int_43 { get; set; }

        public int? ci_cust_int_44 { get; set; }

        public int? ci_cust_int_45 { get; set; }

        public int? ci_cust_int_46 { get; set; }

        public int? ci_cust_int_47 { get; set; }

        public int? ci_cust_int_48 { get; set; }

        public int? ci_cust_int_49 { get; set; }

        public int? ci_cust_int_50 { get; set; }

        [Column(TypeName = "ntext")]
        public string relation_changes { get; set; }

        public DateTime? change_time { get; set; }

        [StringLength(64)]
        public string changed_by { get; set; }

        [StringLength(64)]
        public string problem_type { get; set; }

        [StringLength(64)]
        public string problem_sub_type { get; set; }

        [StringLength(64)]
        public string third_level_category { get; set; }

        public int? ci_sub_type { get; set; }
    }
}
