namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class measurements_lists
    {
        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        [Required]
        [StringLength(64)]
        public string field_name { get; set; }

        [Required]
        [StringLength(64)]
        public string date_field { get; set; }

        [StringLength(64)]
        public string sr_types { get; set; }

        [Column(TypeName = "ntext")]
        public string include_statuses { get; set; }

        [Column(TypeName = "ntext")]
        public string filter_sql { get; set; }

        [Column(TypeName = "ntext")]
        public string filter_xml { get; set; }

        [Column(TypeName = "ntext")]
        public string filter_expression { get; set; }

        public int? status_class { get; set; }

        public int version { get; set; }
    }
}
