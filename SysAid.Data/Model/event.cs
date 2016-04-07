namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("event")]
    public partial class _event
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string computer_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(64)]
        public string log_type { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime event_time { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int record_number { get; set; }

        public int event_id { get; set; }

        [Required]
        [StringLength(64)]
        public string event_type { get; set; }

        [Required]
        [StringLength(64)]
        public string event_source { get; set; }

        [StringLength(64)]
        public string event_category { get; set; }

        [StringLength(64)]
        public string event_username { get; set; }

        [StringLength(64)]
        public string event_domain { get; set; }

        [StringLength(64)]
        public string event_computer { get; set; }

        [StringLength(255)]
        public string event_description { get; set; }

        [Column(TypeName = "image")]
        public byte[] event_binary { get; set; }

        [Column(TypeName = "ntext")]
        public string event_char_data { get; set; }
    }
}
