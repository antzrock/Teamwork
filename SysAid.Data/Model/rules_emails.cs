namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class rules_emails
    {
        [Key]
        public int rule_id { get; set; }

        [Required]
        [StringLength(64)]
        public string rule_name { get; set; }

        [Column(TypeName = "ntext")]
        public string email_addresses { get; set; }

        [Column(TypeName = "ntext")]
        public string rule_desc { get; set; }

        [Column(TypeName = "ntext")]
        public string text_to_search { get; set; }

        [Column(TypeName = "ntext")]
        public string text_to_search_print { get; set; }

        [StringLength(1)]
        public string rule_enabled { get; set; }

        public int position { get; set; }

        [StringLength(64)]
        public string change_to_assign { get; set; }

        [StringLength(64)]
        public string change_to_admin_group { get; set; }

        [StringLength(64)]
        public string change_to_status { get; set; }

        [StringLength(64)]
        public string change_to_main_category { get; set; }

        [StringLength(64)]
        public string change_to_second_category { get; set; }

        [StringLength(64)]
        public string change_to_third_category { get; set; }

        [StringLength(64)]
        public string change_to_urgency { get; set; }

        [StringLength(64)]
        public string change_to_impact { get; set; }

        [StringLength(64)]
        public string change_to_priority { get; set; }

        [Column(TypeName = "ntext")]
        public string action_builder_data { get; set; }

        [Column(TypeName = "ntext")]
        public string action_builder_data_print { get; set; }
    }
}
