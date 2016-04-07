namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class uss_security_questions
    {
        public int id { get; set; }

        [Required]
        [StringLength(32)]
        public string account_id { get; set; }

        [Required]
        [StringLength(255)]
        public string security_question { get; set; }

        [StringLength(1)]
        public string visible { get; set; }

        [StringLength(1)]
        public string mandatory { get; set; }
    }
}
