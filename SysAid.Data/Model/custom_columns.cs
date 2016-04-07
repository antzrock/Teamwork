namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class custom_columns
    {
        public int id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        [Required]
        [StringLength(64)]
        public string entity_type { get; set; }

        [Required]
        [StringLength(64)]
        public string field_caption { get; set; }

        [Required]
        [StringLength(64)]
        public string field_type { get; set; }

        [Required]
        [StringLength(64)]
        public string attribute_name { get; set; }

        [Required]
        [StringLength(64)]
        public string addon_db_name { get; set; }

        [StringLength(1)]
        public string compatibility_mode { get; set; }

        [Column(TypeName = "ntext")]
        public string write_in_list { get; set; }

        [Column(TypeName = "ntext")]
        public string write_in_form { get; set; }

        [Column(TypeName = "ntext")]
        public string write_in_form_mobile { get; set; }

        [Column(TypeName = "ntext")]
        public string read_data_from_form { get; set; }

        [Column(TypeName = "ntext")]
        public string validation_in_form { get; set; }

        [Column(TypeName = "ntext")]
        public string hidden_control_in_form { get; set; }

        [StringLength(1)]
        public string upload_from_file { get; set; }
    }
}
