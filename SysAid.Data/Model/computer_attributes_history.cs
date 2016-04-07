namespace SysAid.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class computer_attributes_history
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string account_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string computer_id { get; set; }

        [StringLength(255)]
        public string cpu_vendor { get; set; }

        [StringLength(255)]
        public string cpu_model { get; set; }

        public int? cpu_speed { get; set; }

        [StringLength(255)]
        public string bios_type { get; set; }

        [StringLength(255)]
        public string display_adapter { get; set; }

        public int? display_memory { get; set; }

        [StringLength(255)]
        public string display_resolution { get; set; }

        [StringLength(255)]
        public string os_type { get; set; }

        [StringLength(255)]
        public string os_version { get; set; }

        [StringLength(255)]
        public string os_service_pack { get; set; }

        public decimal? memory_physical { get; set; }

        [StringLength(64)]
        public string serial { get; set; }

        [StringLength(64)]
        public string model { get; set; }

        [StringLength(64)]
        public string manufacturer { get; set; }

        public DateTime? purchase_date { get; set; }

        public DateTime? warranty_expiration { get; set; }

        public DateTime? last_maintenance { get; set; }

        public int? last_page_count { get; set; }

        public int? maintenance_page_count { get; set; }

        public int? disks_size { get; set; }

        public int? disks_count { get; set; }

        public int? mem_banks { get; set; }

        public int? occupied_mem_banks { get; set; }

        public int? free_mem_banks { get; set; }

        public int? cpu_count { get; set; }

        [StringLength(255)]
        public string os_name { get; set; }

        [StringLength(64)]
        public string os_platform { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int version { get; set; }
    }
}
