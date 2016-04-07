using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class ImpedimentConfiguration : EntityBaseConfiguration<Impediment>
    {
        public ImpedimentConfiguration()
        {
            Property(p => p.WeekNo).IsRequired();
            Property(p => p.Description).IsRequired().HasMaxLength(1000);
            Property(p => p.Effect).IsRequired().HasMaxLength(1000);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            HasRequired(p => p.Project).WithMany(p => p.Impediments).HasForeignKey(i => i.ProjectID).WillCascadeOnDelete(false);
            HasRequired(p => p.WeekReportDetail).WithMany(wrd => wrd.Impediments).HasForeignKey(i => i.WeekReportDetailID).WillCascadeOnDelete(false);
        }
    }
}
