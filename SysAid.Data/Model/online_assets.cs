namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class online_assets
    {
        [Key]
        [StringLength(64)]
        public string asset_id { get; set; }

        public int? is_online { get; set; }

        public DateTime? last_update { get; set; }
    }
}
