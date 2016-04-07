namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class share_and_compare
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int row_index { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        public DateTime? validity_date { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string sql_stat1_1 { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string sql_stat1_2 { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string sql_stat2_1 { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string sql_stat2_2 { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string sql_stat3_1 { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string sql_stat3_2 { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string sql_stat4_1 { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string sql_stat4_2 { get; set; }

        [StringLength(255)]
        public string industries_list { get; set; }

        [StringLength(255)]
        public string filter_name { get; set; }

        [StringLength(255)]
        public string filter_expression { get; set; }

        [Column(TypeName = "ntext")]
        public string filter_node { get; set; }

        [StringLength(255)]
        public string default_filter_name { get; set; }

        [StringLength(255)]
        public string default_filter_expression { get; set; }

        [Column(TypeName = "ntext")]
        public string default_filter_node { get; set; }

        [StringLength(64)]
        public string filter_type { get; set; }

        public double? value { get; set; }

        public double? min_value { get; set; }

        public double? max_value { get; set; }

        [StringLength(1)]
        public string share_stat { get; set; }

        [StringLength(1)]
        public string more_is_green { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string notes { get; set; }

        [Required]
        [StringLength(255)]
        public string info_url { get; set; }

        [StringLength(64)]
        public string unit_type { get; set; }

        [StringLength(1)]
        public string filter_relevance { get; set; }

        public int? value_data_1 { get; set; }

        public int? value_data_2 { get; set; }

        public int? value_data_3 { get; set; }

        public int? value_data_4 { get; set; }

        public int? value_data_5 { get; set; }

        public int? value_data_6 { get; set; }

        public int? value_data_7 { get; set; }

        public int? value_data_8 { get; set; }

        public int? value_data_9 { get; set; }

        public int? value_data_10 { get; set; }

        public int? value_data_11 { get; set; }

        public int? value_data_12 { get; set; }

        public int? value_data_13 { get; set; }

        public int? value_data_14 { get; set; }

        public int? value_data_15 { get; set; }

        public int? value_data_16 { get; set; }

        public int? value_data_17 { get; set; }

        public int? value_data_18 { get; set; }

        public int? value_data_19 { get; set; }

        public int? value_data_20 { get; set; }

        public int? value_data_21 { get; set; }

        public int? value_data_22 { get; set; }

        public int? value_data_23 { get; set; }

        public int? value_data_24 { get; set; }

        public int? value_data_25 { get; set; }

        public int? value_data_26 { get; set; }

        public int? value_data_27 { get; set; }

        public int? value_data_28 { get; set; }

        public int? value_data_29 { get; set; }

        public int? value_data_30 { get; set; }

        public int? value_data_31 { get; set; }

        public int? value_data_32 { get; set; }

        public int? value_data_33 { get; set; }

        public int? value_data_34 { get; set; }

        public int? value_data_35 { get; set; }

        public int? value_data_36 { get; set; }

        public int? value_data_37 { get; set; }

        public int? value_data_38 { get; set; }

        public int? value_data_39 { get; set; }

        public int? value_data_40 { get; set; }

        public int? value_data_41 { get; set; }

        public int? value_data_42 { get; set; }

        public int? value_data_43 { get; set; }

        public int? value_data_44 { get; set; }

        public int? value_data_45 { get; set; }

        public int? value_data_46 { get; set; }

        public int? value_data_47 { get; set; }

        public int? value_data_48 { get; set; }

        public int? value_data_49 { get; set; }

        public int? value_data_50 { get; set; }
    }
}
