namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QRTZ_JOB_DETAILS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QRTZ_JOB_DETAILS()
        {
            QRTZ_JOB_LISTENERS = new HashSet<QRTZ_JOB_LISTENERS>();
            QRTZ_TRIGGERS = new HashSet<QRTZ_TRIGGERS>();
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(80)]
        public string JOB_NAME { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(80)]
        public string JOB_GROUP { get; set; }

        [StringLength(120)]
        public string DESCRIPTION { get; set; }

        [Required]
        [StringLength(128)]
        public string JOB_CLASS_NAME { get; set; }

        [Required]
        [StringLength(1)]
        public string IS_DURABLE { get; set; }

        [Required]
        [StringLength(1)]
        public string IS_VOLATILE { get; set; }

        [Required]
        [StringLength(1)]
        public string IS_STATEFUL { get; set; }

        [Required]
        [StringLength(1)]
        public string REQUESTS_RECOVERY { get; set; }

        [Column(TypeName = "image")]
        public byte[] JOB_DATA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QRTZ_JOB_LISTENERS> QRTZ_JOB_LISTENERS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QRTZ_TRIGGERS> QRTZ_TRIGGERS { get; set; }
    }
}
