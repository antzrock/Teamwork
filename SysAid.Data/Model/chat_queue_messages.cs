namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class chat_queue_messages
    {
        public int id { get; set; }

        [Required]
        [StringLength(64)]
        public string title { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string message { get; set; }

        public int queue { get; set; }
    }
}
