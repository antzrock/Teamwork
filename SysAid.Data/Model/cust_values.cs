namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class cust_values
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string list_name { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int value_key { get; set; }

        [Required]
        [StringLength(255)]
        public string value_caption { get; set; }

        public int? value_class { get; set; }

        public int? module_relevance { get; set; }

        [StringLength(255)]
        public string valid_for_user_group { get; set; }
    }
}
