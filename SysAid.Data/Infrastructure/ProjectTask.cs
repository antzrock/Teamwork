using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAid.Data.Infrastructure
{
    public class ProjectTask
    {
        public int id { get; set; }

        public int? project_id { get; set; }

        public string project_title { get; set; }

        public string project_description { get; set; }

        public string project_manager { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public int? category { get; set; }

        public string category_str { get; set; }

        public int? status { get; set; }

        public string status_str { get; set; }

        public int? progress { get; set; }
        

        public string notes { get; set; }

        public DateTime? start_time { get; set; }

        public DateTime? end_time { get; set; }

        public int? estimation { get; set; }
    }
}
