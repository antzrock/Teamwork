namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class message
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string user_name { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime sent_time { get; set; }

        [StringLength(64)]
        public string sender { get; set; }

        public int recv_flag { get; set; }

        [Column(TypeName = "ntext")]
        public string msg { get; set; }

        [StringLength(64)]
        public string msgid { get; set; }

        public int? service_request_id { get; set; }

        [StringLength(1)]
        public string display_to_user { get; set; }
    }
}
