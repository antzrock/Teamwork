namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class software2install_name
    {
        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int software_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string install_name { get; set; }
    }
}
