namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class customized_snmp_oids
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string oid { get; set; }

        [Required]
        [StringLength(255)]
        public string display_name { get; set; }

        public int? mapped_field { get; set; }

        [StringLength(255)]
        public string filter_name { get; set; }

        [StringLength(255)]
        public string filter_expression { get; set; }

        [Column(TypeName = "ntext")]
        public string filter_node { get; set; }

        [StringLength(1)]
        public string is_writable { get; set; }

        [StringLength(64)]
        public string addon_db_name { get; set; }
    }
}
