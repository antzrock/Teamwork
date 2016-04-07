namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class predefined_network_check
    {
        public int id { get; set; }

        [StringLength(64)]
        public string name { get; set; }

        public int? port { get; set; }

        [StringLength(64)]
        public string notification { get; set; }

        public int? is_monitor { get; set; }
    }
}
