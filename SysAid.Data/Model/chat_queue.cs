namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class chat_queue
    {
        public int id { get; set; }

        [Required]
        [StringLength(64)]
        public string name { get; set; }

        [StringLength(64)]
        public string group_name { get; set; }

        [Column(TypeName = "ntext")]
        public string welcome_message { get; set; }

        [Column(TypeName = "ntext")]
        public string welcome_message_from_agent { get; set; }

        [Column(TypeName = "ntext")]
        public string idle_message { get; set; }

        [Column(TypeName = "ntext")]
        public string operator_accept_message { get; set; }

        [Column(TypeName = "ntext")]
        public string operator_release_message { get; set; }

        [Column(TypeName = "ntext")]
        public string operator_replace_message { get; set; }

        [StringLength(64)]
        public string offline_image_url { get; set; }

        [StringLength(64)]
        public string online_image_url { get; set; }

        [StringLength(1)]
        public string add_hour_in_chat_session { get; set; }

        [Column(TypeName = "ntext")]
        public string embed_in_site_script { get; set; }

        public int? time_before_idle { get; set; }

        public int? time_before_close { get; set; }

        [StringLength(1)]
        public string allow_offline_chat { get; set; }

        [Column(TypeName = "ntext")]
        public string submit_offline_chat_message { get; set; }

        [StringLength(1)]
        public string display_details_screen { get; set; }

        [StringLength(256)]
        public string email_address { get; set; }

        [Column(TypeName = "ntext")]
        public string operator_close_message { get; set; }
    }
}
