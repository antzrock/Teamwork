using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class TeamStatusConfiguration : EntityBaseConfiguration<TeamStatus>
    {
        public TeamStatusConfiguration()
        {
            Property(ps => ps.Name).IsRequired().HasMaxLength(100);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            HasMany(ts => ts.Teams).WithRequired(t => t.Status).WillCascadeOnDelete(false);
        }
    }
}
