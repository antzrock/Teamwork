using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class RiskConfiguration : EntityBaseConfiguration<Risk>
    {
        public RiskConfiguration()
        {
            Property(p => p.WeekNo).IsRequired();
            Property(p => p.Description).IsRequired().HasMaxLength(1000);
            Property(p => p.Effect).IsRequired().HasMaxLength(1000);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            HasRequired(r => r.WeekReportDetail).WithMany(wrd => wrd.Risks).HasForeignKey(r => r.WeekReportDetailID).WillCascadeOnDelete(false);
            HasRequired(r => r.Project).WithMany(p => p.Risks).HasForeignKey(r => r.ProjectID).WillCascadeOnDelete(false);
        }
    }
}
