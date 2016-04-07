using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class WeekReportDetailConfiguration : EntityBaseConfiguration<WeekReportDetail>
    {
        public WeekReportDetailConfiguration()
        {
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            HasRequired(p => p.WeekReportHeader).WithMany(wrh => wrh.Details).HasForeignKey(wrd => wrd.WeekReportHeaderID).WillCascadeOnDelete(false);
            HasMany(wrd => wrd.KeyAchievements).WithRequired(ka => ka.WeekReportDetail).HasForeignKey(wrd => wrd.WeekReportDetailID).WillCascadeOnDelete(false);
            HasRequired(wrd => wrd.Project).WithMany(p => p.WeekReportDetails).HasForeignKey(p => p.ProjectID).WillCascadeOnDelete(false);
            HasMany(wrd => wrd.PlannedActivities).WithRequired(pa => pa.WeekReportDetail).HasForeignKey(pa => pa.WeekReportDetailID).WillCascadeOnDelete(false);
            HasMany(wrd => wrd.Risks).WithRequired(r => r.WeekReportDetail).HasForeignKey(r => r.WeekReportDetailID).WillCascadeOnDelete(false);
            HasMany(wrd => wrd.Impediments).WithRequired(i => i.WeekReportDetail).HasForeignKey(wrd => wrd.WeekReportDetailID).WillCascadeOnDelete(false);
        }
    }
}
