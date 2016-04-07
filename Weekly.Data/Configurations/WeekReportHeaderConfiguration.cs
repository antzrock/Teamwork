using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class WeekReportHeaderConfiguration : EntityBaseConfiguration<WeekReportHeader>
    {
        public WeekReportHeaderConfiguration()
        {
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            Property(p => p.isGroupReport).IsRequired();
            HasRequired(p => p.Week).WithMany(w => w.WeeklyReports).HasForeignKey(wr => wr.WeekID).WillCascadeOnDelete(false);
            HasRequired(p => p.User).WithMany(u => u.WeeklyReports).HasForeignKey(p => p.UserID).WillCascadeOnDelete(false);
            HasMany(wrh => wrh.Details).WithRequired(wrd => wrd.WeekReportHeader).HasForeignKey(wrd => wrd.WeekReportHeaderID).WillCascadeOnDelete(false);
        }
    }
}
