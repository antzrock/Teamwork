namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class gfi_products
    {
        [Key]
        public int product_id { get; set; }

        [Required]
        [StringLength(255)]
        public string vendor { get; set; }

        [StringLength(255)]
        public string product_name { get; set; }
    }
}
