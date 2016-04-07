namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_tab_dependences
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

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sub_tab_order { get; set; }

        [StringLength(64)]
        public string depends_on_tab { get; set; }

        public int? depends_on_sub { get; set; }

        [StringLength(64)]
        public string dependent_method { get; set; }

        [StringLength(255)]
        public string add_depends { get; set; }

        [Column(TypeName = "ntext")]
        public string filter_expression { get; set; }

        [Column(TypeName = "ntext")]
        public string filter_sql { get; set; }
    }
}
