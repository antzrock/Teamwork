using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class KeyAchievementConfiguration : EntityBaseConfiguration<KeyAchievement>
    {
        public KeyAchievementConfiguration()
        {
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            Property(p => p.WeekNo).IsRequired();
            Property(p => p.Description).IsRequired().HasMaxLength(1000);
            Property(p => p.PercentComplete).IsRequired();
            HasRequired(ka => ka.WeekReportDetail).WithMany(wrd => wrd.KeyAchievements).HasForeignKey(ka => ka.WeekReportDetailID).WillCascadeOnDelete(false);
            HasRequired(ka => ka.Project).WithMany(p => p.KeyAchievements).HasForeignKey(ka => ka.ProjectID).WillCascadeOnDelete(false);
        }
    }
}
