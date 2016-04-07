namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class service_req_msg
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime msg_time { get; set; }

        [StringLength(64)]
        public string from_user { get; set; }

        [StringLength(255)]
        public string to_user { get; set; }

        [StringLength(255)]
        public string cc_user { get; set; }

        [StringLength(64)]
        public string method { get; set; }

        [Column(TypeName = "ntext")]
        public string subject { get; set; }

        [StringLength(64)]
        public string msgid { get; set; }

        [Column(TypeName = "ntext")]
        public string msg_body { get; set; }
    }
}
