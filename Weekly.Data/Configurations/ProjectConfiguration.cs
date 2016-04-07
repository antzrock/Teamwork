using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Data.Configurations
{
    public class ProjectConfiguration : EntityBaseConfiguration<Project>
    {
        public ProjectConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(100);
            Property(p => p.Code).IsRequired().HasMaxLength(50);
            Property(p => p.ExecutiveSummary).IsRequired().HasMaxLength(1000);
            Property(p => p.CreatedBy).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            Property(p => p.isGroupProject).IsRequired();
            Ignore(p => p.CurrentActivityStatus);
            Ignore(p => p.CurrentActivityName);
            HasMany(p => p.TeamMembers).WithRequired(p => p.Project).HasForeignKey(p => p.ProjectID).WillCascadeOnDelete(false);
            HasMany(p => p.ActivitiesSchedule).WithRequired(p => p.Project).HasForeignKey(p => p.ProjectID).WillCascadeOnDelete(false);
            HasOptional(p => p.MainProject).WithMany(p => p.SubProjects).HasForeignKey(p => p.MainProjectID).WillCascadeOnDelete(false);
            HasRequired(p => p.Group).WithMany(g => g.Projects).HasForeignKey(p => p.GroupID).WillCascadeOnDelete(false);
            HasOptional(p => p.Team).WithMany(t => t.Projects).HasForeignKey(p => p.TeamID).WillCascadeOnDelete(false);
            HasRequired(p => p.Status).WithMany(ps => ps.Projects).HasForeignKey(p => p.ProjectStatusID).WillCascadeOnDelete(false);
            HasMany(p => p.WeekReportDetails).WithRequired(wrd => wrd.Project).HasForeignKey(wrd => wrd.ProjectID).WillCascadeOnDelete(false);
            HasMany(p => p.KeyAchievements).WithRequired(ka => ka.Project).HasForeignKey(ka => ka.ProjectID).WillCascadeOnDelete(false);
            HasMany(p => p.PlannedActivities).WithRequired(pa => pa.Project).HasForeignKey(pa => pa.ProjectID).WillCascadeOnDelete(false);
            HasMany(p => p.Risks).WithRequired(r => r.Project).HasForeignKey(r => r.ProjectID).WillCascadeOnDelete(false);
            HasMany(p => p.Impediments).WithRequired(i => i.Project).HasForeignKey(i => i.ProjectID).WillCascadeOnDelete(false);
            HasRequired(p => p.Category).WithMany(pc => pc.Projects).HasForeignKey(pc => pc.ProjectCategoryID).WillCascadeOnDelete(false);
            HasOptional(p => p.ProductBacklog).WithRequired(pb => pb.Project).WillCascadeOnDelete(false);

        }
    }
}
