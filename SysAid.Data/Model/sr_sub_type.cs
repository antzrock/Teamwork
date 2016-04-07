namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_sub_type
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sr_type { get; set; }

        [Key]
        [Column("sr_sub_type", Order = 2)]
        public int sr_sub_type1 { get; set; }

        [Required]
        [StringLength(255)]
        public string sub_type_name { get; set; }

        [StringLength(255)]
        public string sub_type_class { get; set; }

        [Column(TypeName = "ntext")]
        public string sub_type_form_view { get; set; }

        [Column(TypeName = "ntext")]
        public string end_user_view { get; set; }
    }
}
