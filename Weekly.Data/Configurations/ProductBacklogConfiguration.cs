using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class ProductBacklogConfiguration : EntityBaseConfiguration<ProductBacklog>
    {
        public ProductBacklogConfiguration()
        {
            Property(t => t.CreatedBy).IsRequired();
            Property(t => t.CreatedDate).IsRequired();
            HasRequired(pb => pb.Project).WithOptional(p => p.ProductBacklog).WillCascadeOnDelete(false);
        }
    }
}
