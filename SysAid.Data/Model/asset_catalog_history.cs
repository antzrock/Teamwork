namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class asset_catalog_history
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string catalog_number { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [StringLength(64)]
        public string model { get; set; }

        [StringLength(64)]
        public string manufacturer { get; set; }

        public int? supplier_id { get; set; }

        [Column(TypeName = "ntext")]
        public string notes { get; set; }

        public DateTime? cust_date1 { get; set; }

        public DateTime? cust_date2 { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int version { get; set; }

        public DateTime? change_time { get; set; }

        [StringLength(64)]
        public string changed_by { get; set; }
    }
}
