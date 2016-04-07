namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ui_menus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int mi_id { get; set; }

        [StringLength(64)]
        public string mi_name { get; set; }

        public int? mi_parent { get; set; }

        [StringLength(32)]
        public string mi_action_type { get; set; }

        [StringLength(255)]
        public string mi_action { get; set; }

        [StringLength(1)]
        public string mi_enabled { get; set; }

        [StringLength(64)]
        public string mi_ui_class { get; set; }

        public byte? mi_module_id { get; set; }

        public int? mi_order { get; set; }

        public int? mi_permission { get; set; }

        public int? mi_edition { get; set; }
    }
}
