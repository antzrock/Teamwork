namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sysaid_item_links
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string item_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int item_type { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(64)]
        public string linked_item_id { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int linked_item_type { get; set; }

        [StringLength(1)]
        public string is_main_linked_item { get; set; }

        [StringLength(64)]
        public string cause_item { get; set; }
    }
}
