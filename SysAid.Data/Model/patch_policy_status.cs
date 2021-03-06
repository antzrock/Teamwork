namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class patch_policy_status
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int policy_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string patch_id { get; set; }

        public int patch_status { get; set; }
    }
}
