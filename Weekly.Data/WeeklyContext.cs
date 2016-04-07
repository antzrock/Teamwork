using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Data.Configurations;
using Weekly.Entities;

namespace Weekly.Data
{
    public class TeamworkContext : DbContext
    {
        public TeamworkContext() 
            :base("teamworkconnection")
        {
            Database.SetInitializer<TeamworkContext>(null);
        }

        #region Entity Sets
        public IDbSet<User> UserSet { get; set; }
        public IDbSet<Role> RoleSet { get; set; }
        public IDbSet<UserRole> UserRoleSet { get; set; }
        public IDbSet<Group> GroupSet { get; set; }
        public IDbSet<GroupMember> GroupMemberSet { get; set; }
        public IDbSet<GroupRole> GroupRoleSet { get; set; }
        public IDbSet<Team> TeamSet { get; set; }
        public IDbSet<TeamMember> TeamMemberSet { get; set; }
        public IDbSet<TeamRole> TeamRoleSet { get; set; }
        public IDbSet<Week> WeekSet { get; set; }
        public IDbSet<Project> ProjectSet { get; set; }
        public IDbSet<ScrumRole> ScrumRoleSet { get; set; }
        public IDbSet<ScrumTeamMember> ScrumTeamMemberSet { get; set; }
        public IDbSet<ProjectSchedule> ProjectScheduleSet { get; set; }
        public IDbSet<MemberStatus> MemberStatusSet { get; set; }
        public IDbSet<UserStatus> UserStatusSet { get; set; }
        public IDbSet<ProjectScheduleStatus> ProjectScheduleStatusSet { get; set; }
        public IDbSet<ProjectScheduleActivityStatus> ProjectScheduleActivityStatus { get; set; }
        public IDbSet<ProjectStatus> ProjectStatusSet { get; set; }
        public IDbSet<WeekReportHeader> WeekReportHeaderSet { get; set; }
        public IDbSet<WeekReportDetail> WeekReportDetailSet { get; set; }
        public IDbSet<KeyAchievement> KeyAchievementSet { get; set; }
        public IDbSet<PlannedActivity> PlannedActivitySet { get; set; }
        public IDbSet<Risk> RiskSet { get; set; }
        public IDbSet<Impediment> ImpedimentSet { get; set; }

        public IDbSet<Permission> PermissionSet { get; set; }

        public IDbSet<ProjectCategory> ProjectCategorySet { get; set; }

        public IDbSet<GroupStatus> GroupStatusSet { get; set; }

        public IDbSet<TeamStatus> TeamStatusSet { get; set; }

        public IDbSet<Entities.Task> TaskSet { get; set; }

        public IDbSet<Entities.TaskStatus> TaskStatusSet { get; set; }

        public IDbSet<ProductBacklog> ProductBacklogSet { get; set; }

        public IDbSet<BacklogItem> BacklogItemSet { get; set; }

        public IDbSet<BacklogItemStatus> BacklogItemStatusSet { get; set; }

        public IDbSet<BacklogItemPriority> BacklogItemPrioritySet { get; set; }
        #endregion

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Add configurations here...
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new GroupMemberConfiguration());
            modelBuilder.Configurations.Add(new GroupRoleConfiguration());
            modelBuilder.Configurations.Add(new TeamConfigurations());
            modelBuilder.Configurations.Add(new TeamMemberConfiguration());
            modelBuilder.Configurations.Add(new TeamRoleConfiguration());
            modelBuilder.Configurations.Add(new WeekConfiguration());
            modelBuilder.Configurations.Add(new ProjectConfiguration());
            modelBuilder.Configurations.Add(new ScrumRoleConfiguration());
            modelBuilder.Configurations.Add(new ScrumTeamMemberConfiguration());
            modelBuilder.Configurations.Add(new ProjectScheduleConfiguration());
            modelBuilder.Configurations.Add(new MemberStatusConfiguration());
            modelBuilder.Configurations.Add(new UserStatusConfiguration());
            modelBuilder.Configurations.Add(new ProjectScheduleStatusConfiguration());
            modelBuilder.Configurations.Add(new ProjectScheduleActivityStatusConfiguration());
            modelBuilder.Configurations.Add(new ProjectStatusConfiguration());
            modelBuilder.Configurations.Add(new WeekReportHeaderConfiguration());
            modelBuilder.Configurations.Add(new WeekReportDetailConfiguration());
            modelBuilder.Configurations.Add(new KeyAchievementConfiguration());
            modelBuilder.Configurations.Add(new PlannedActivityConfiguration());
            modelBuilder.Configurations.Add(new RiskConfiguration());
            modelBuilder.Configurations.Add(new ImpedimentConfiguration());
            modelBuilder.Configurations.Add(new PermissionConfiguration());
            modelBuilder.Configurations.Add(new ProjectCategoryConfiguration());
            modelBuilder.Configurations.Add(new GroupStatusConfiguration());
            modelBuilder.Configurations.Add(new TeamStatusConfiguration());
            modelBuilder.Configurations.Add(new TaskConfiguration());
            modelBuilder.Configurations.Add(new TaskStatusConfiguration());
            modelBuilder.Configurations.Add(new ProductBacklogConfiguration());
            modelBuilder.Configurations.Add(new BacklogItemStatusConfiguration());
            modelBuilder.Configurations.Add(new BacklogItemPriorityConfiguration());
            modelBuilder.Configurations.Add(new BacklogItemConfiguration());
        }
    }
}
