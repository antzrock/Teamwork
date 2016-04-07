namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_sub_tab_populate
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

        [Key]
        [Column(Order = 4)]
        [StringLength(64)]
        public string field_name { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(64)]
        public string source_tab_name { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int source_sub_tab_order { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(64)]
        public string source_field_name { get; set; }
    }
}
