namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class survey_questions
    {
        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        public int id { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string question_text { get; set; }

        [Required]
        [StringLength(1)]
        public string enabled { get; set; }

        [Required]
        [StringLength(1)]
        public string display_comment { get; set; }

        public int position { get; set; }
    }
}
