using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAid.Data.Infrastructure
{
    public class ServiceRequest
    {
        public int id { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public int? status { get; set; }

        public string status_str { get; set; }

        public string request_user { get; set; }

        public DateTime? insert_time { get; set; }

        public DateTime? update_time { get; set; }

        public DateTime? close_time { get; set; }

        public DateTime? due_date { get; set; }
    }
}
