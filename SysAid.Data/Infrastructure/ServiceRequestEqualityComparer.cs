using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAid.Data.Infrastructure
{
    public class ServiceRequestEqualityComparer : IEqualityComparer<ServiceRequest>
    {
        public bool Equals(ServiceRequest x, ServiceRequest y)
        {
            return x.id == y.id;
        }

        public int GetHashCode(ServiceRequest obj)
        {
            return obj.id.GetHashCode();
        }
    }
}
