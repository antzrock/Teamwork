namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class mdm_actions
    {
        [Key]
        [StringLength(64)]
        public string computer_id { get; set; }

        public int? action_code { get; set; }

        [StringLength(255)]
        public string push_message { get; set; }

        public int? status { get; set; }

        public DateTime? insert_time { get; set; }
    }
}
