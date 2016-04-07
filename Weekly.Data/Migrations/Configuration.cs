namespace Weekly.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Weekly.Data.TeamworkContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Weekly.Data.TeamworkContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            DateTime dateToday = System.DateTime.Now;

            //Add Application Permissions....
            context.PermissionSet.AddOrUpdate(
               p => p.Name,
               new Entities.Permission { Name = "CREATE_GROUP", Description = "Can add new group.", isAppLevel = true, isGroupLevel = false, isTeamLevel = false, isProjectLevel = false, CreatedBy = 1, CreatedDate = dateToday },
               new Entities.Permission { Name = "UPDATE_GROUP", Description = "Can edit existing group.", isAppLevel = true, isGroupLevel = true, isTeamLevel = false, isProjectLevel = false, CreatedBy = 1, CreatedDate = dateToday },
               new Entities.Permission { Name = "DELETE_GROUP", Description = "Can delete existing group.", isAppLevel = true, isGroupLevel = false, isTeamLevel = false, isProjectLevel = false, CreatedBy = 1, CreatedDate = dateToday },
               new Entities.Permission { Name = "READ_GROUP", Description = "Can view group details.", isAppLevel = true, isGroupLevel = true, isTeamLevel = false, isProjectLevel = false, CreatedBy = 1, CreatedDate = dateToday },

               new Entities.Permission { Name = "CREATE_TEAM", Description = "Can add new team to an existing group.", isAppLevel = true, isGroupLevel = true, isTeamLevel = false, isProjectLevel = false, CreatedBy = 1, CreatedDate = dateToday },
               new Entities.Permission { Name = "UPDATE_TEAM", Description = "Can edit existing team.", isAppLevel = true, isGroupLevel = true, isTeamLevel = true, isProjectLevel = false, CreatedBy = 1, CreatedDate = dateToday },
               new Entities.Permission { Name = "DELETE_TEAM", Description = "Can delete existing team.", isAppLevel = true, isGroupLevel = true, isTeamLevel = false, isProjectLevel = false, CreatedBy = 1, CreatedDate = dateToday },
               new Entities.Permission { Name = "READ_TEAM", Description = "Can view team details.", isAppLevel = true, isGroupLevel = true, isTeamLevel = true, isProjectLevel = false, CreatedBy = 1, CreatedDate = dateToday },

               new Entities.Permission { Name = "CREATE_GROUP_PROJECT", Description = "Can add new group project to an existing group.", isAppLevel = true, isGroupLevel = true, isTeamLevel = true, isProjectLevel = true, CreatedBy = 1, CreatedDate = dateToday },
               new Entities.Permission { Name = "UPDATE_GROUP_PROJECT", Description = "Can edit existing group project.", isAppLevel = true, isGroupLevel = true, isTeamLevel = true, isProjectLevel = true, CreatedBy = 1, CreatedDate = dateToday },
               new Entities.Permission { Name = "DELETE_GROUP_PROJECT", Description = "Can delete existing group project.", isAppLevel = true, isGroupLevel = true, isTeamLevel = true, isProjectLevel = false, CreatedBy = 1, CreatedDate = dateToday },
               new Entities.Permission { Name = "READ_GROUP_PROJECT", Description = "Can view existing group project.", isAppLevel = true, isGroupLevel = true, isTeamLevel = true, isProjectLevel = true, CreatedBy = 1, CreatedDate = dateToday },

               new Entities.Permission { Name = "CREATE_TEAM_PROJECT", Description = "Can add new team project to an existing team.", isAppLevel = true, isGroupLevel = true, isTeamLevel = true, isProjectLevel = true, CreatedBy = 1, CreatedDate = dateToday },
               new Entities.Permission { Name = "UPDATE_TEAM_PROJECT", Description = "Can edit existing team project.", isAppLevel = true, isGroupLevel = true, isTeamLevel = true, isProjectLevel = true, CreatedBy = 1, CreatedDate = dateToday },
               new Entities.Permission { Name = "DELETE_TEAM_PROJECT", Description = "Can delete existing team project.", isAppLevel = true, isGroupLevel = true, isTeamLevel = true, isProjectLevel = false, CreatedBy = 1, CreatedDate = dateToday },
               new Entities.Permission { Name = "READ_TEAM_PROJECT", Description = "Can view existing team project.", isAppLevel = true, isGroupLevel = true, isTeamLevel = true, isProjectLevel = true, CreatedBy = 1, CreatedDate = dateToday }

               );

            List<Entities.Permission> ApplicationAdminPermissions = context.PermissionSet.Where(p => p.isAppLevel == true).ToList();

            //Add Application Roles...
            context.RoleSet.AddOrUpdate(
               r => r.Name,
               new Entities.Role { Name = "SuperAdmin", CreatedBy = 1, CreatedDate = dateToday, DefaultPermissions = ApplicationAdminPermissions, isSystemGenerated = true },
               new Entities.Role { Name = "User", CreatedBy = 1, CreatedDate = dateToday, isSystemGenerated = true }
               );

            //Add Group Status...
            context.GroupStatusSet.AddOrUpdate(
               gs => gs.Name,
               new Entities.GroupStatus { Name = "Active", CreatedBy = 1, CreatedDate = dateToday },
               new Entities.GroupStatus { Name = "Inactive", CreatedBy = 1, CreatedDate = dateToday }

               );

            //Get Active Group Status...
            Entities.GroupStatus ActiveGroupStatus = context.GroupStatusSet.Where(gs => gs.Name == "Active").FirstOrDefault();

            if (ActiveGroupStatus != null)
            {
                //Add Groups...
                context.GroupSet.AddOrUpdate(
                    g => g.Name,
                    new Entities.Group { Name = "Information Technology", Code = "IT", CreatedBy = 1, CreatedDate = dateToday, GroupStatusID = ActiveGroupStatus.ID, Status = ActiveGroupStatus },
                    new Entities.Group { Name = "Commercial", Code = "COMMERCIAL", CreatedBy = 1, CreatedDate = dateToday, GroupStatusID = ActiveGroupStatus.ID, Status = ActiveGroupStatus },
                    new Entities.Group { Name = "Finance", Code = "FINANCE", CreatedBy = 1, CreatedDate = dateToday, GroupStatusID = ActiveGroupStatus.ID, Status = ActiveGroupStatus },
                    new Entities.Group { Name = "HR and Corporate Affairs", Code = "HR", CreatedBy = 1, CreatedDate = dateToday, GroupStatusID = ActiveGroupStatus.ID, Status = ActiveGroupStatus },
                    new Entities.Group { Name = "Supply Chain", Code = "SUPPLY CHAIN", CreatedBy = 1, CreatedDate = dateToday, GroupStatusID = ActiveGroupStatus.ID, Status = ActiveGroupStatus }
                    );
            }

            //Get Group Roles Default Permissions...
            List<Entities.Permission> GroupAdminPermissions = context.PermissionSet.Where(p => p.isGroupLevel == true).ToList();
            List<Entities.Permission> GroupOthersPermissions = context.PermissionSet.Where(p => p.Name == "READ_GROUP" || p.Name == "READ_TEAM" || p.Name == "READ_GROUP_PROJECT" || p.Name == "READ_GROUP_PROJECT").ToList();


            //Add Group Roles...
            context.GroupRoleSet.AddOrUpdate(
                gr => gr.Name,
                new Entities.GroupRole { Name = "GroupManager", CreatedBy = 1, CreatedDate = dateToday, DefaultPermissions = GroupOthersPermissions, isSystemGenerated = true },
                new Entities.GroupRole { Name = "GroupMember", CreatedBy = 1, CreatedDate = dateToday, DefaultPermissions = GroupOthersPermissions, isSystemGenerated = true },
                new Entities.GroupRole { Name = "GroupAdmin", CreatedBy = 1, CreatedDate = dateToday, DefaultPermissions = GroupAdminPermissions, isSystemGenerated = true }
                );

            //Add Team Status...
            context.TeamStatusSet.AddOrUpdate(
                ts => ts.Name,
                new Entities.TeamStatus { Name = "Active", CreatedBy = 1, CreatedDate = dateToday },
                new Entities.TeamStatus { Name = "InActive", CreatedBy = 1, CreatedDate = dateToday }
                );

            //Get active team status...
            Entities.TeamStatus activeTeamStatus = context.TeamStatusSet.Where(ts => ts.Name == "Active").FirstOrDefault();

            // Add Information Teachnology Group Teams...
            Entities.Group ITGroup = new Entities.Group();
            ITGroup = context.GroupSet.Where(g => g.Name == "Information Technology").FirstOrDefault();

            if (ITGroup != null && activeTeamStatus != null)
            {
                context.TeamSet.AddOrUpdate(
                    t => t.Name,
                    new Entities.Team { Name = "Applications Support", GroupID = ITGroup.ID, Group = ITGroup, Code = "APPS", CreatedBy = 1, CreatedDate = dateToday, TeamStatusID = activeTeamStatus.ID, Status = activeTeamStatus },
                    new Entities.Team { Name = "Technical Support", GroupID = ITGroup.ID, Group = ITGroup, Code = "INFRA", CreatedBy = 1, CreatedDate = dateToday, TeamStatusID = activeTeamStatus.ID, Status = activeTeamStatus },
                    new Entities.Team { Name = "Management Team", GroupID = ITGroup.ID, Group = ITGroup, Code = "MANAGERS", CreatedBy = 1, CreatedDate = dateToday, TeamStatusID = activeTeamStatus.ID, Status = activeTeamStatus },
                    new Entities.Team { Name = "Change Advisory Board", GroupID = ITGroup.ID, Group = ITGroup, Code = "CAB", CreatedBy = 1, CreatedDate = dateToday, TeamStatusID = activeTeamStatus.ID, Status = activeTeamStatus }
                    );
            }

            // Add Marketing Group Teams...
            Entities.Group MarketingGroup = new Entities.Group();
            MarketingGroup = context.GroupSet.Where(g => g.Name == "Commercial").FirstOrDefault();

            if (MarketingGroup != null && activeTeamStatus != null)
            {
                context.TeamSet.AddOrUpdate(
                    ts => ts.Name,
                    new Entities.Team { Name = "Sales Team", GroupID = MarketingGroup.ID, Group = MarketingGroup, Code = "Sales Team", CreatedBy = 1, CreatedDate = dateToday, TeamStatusID = activeTeamStatus.ID, Status = activeTeamStatus },
                    new Entities.Team { Name = "Port/Yard Team", GroupID = MarketingGroup.ID, Group = MarketingGroup, Code = "Port/Yard Team", CreatedBy = 1, CreatedDate = dateToday, TeamStatusID = activeTeamStatus.ID, Status = activeTeamStatus }
                    );
            }

            // Add Finance Group Teams...
            Entities.Group FinanceGroup = new Entities.Group();
            FinanceGroup = context.GroupSet.Where(g => g.Name == "Finance").FirstOrDefault();

            if (FinanceGroup != null && activeTeamStatus != null)
            {
                context.TeamSet.AddOrUpdate(
                    ts => ts.Name,
                    new Entities.Team { Name = "Tax and Treasury", GroupID = FinanceGroup.ID, Group = FinanceGroup, Code = "Tax and Treasury", CreatedBy = 1, CreatedDate = dateToday, TeamStatusID = activeTeamStatus.ID, Status = activeTeamStatus },
                    new Entities.Team { Name = "Metals Accounting", GroupID = FinanceGroup.ID, Group = FinanceGroup, Code = "Metals Accounting", CreatedBy = 1, CreatedDate = dateToday, TeamStatusID = activeTeamStatus.ID, Status = activeTeamStatus },
                    new Entities.Team { Name = "Financial Accounting", GroupID = FinanceGroup.ID, Group = FinanceGroup, Code = "Financial Accounting", CreatedBy = 1, CreatedDate = dateToday, TeamStatusID = activeTeamStatus.ID, Status = activeTeamStatus },
                    new Entities.Team { Name = "Management Accounting", GroupID = FinanceGroup.ID, Group = FinanceGroup, Code = "Management Accounting", CreatedBy = 1, CreatedDate = dateToday, TeamStatusID = activeTeamStatus.ID, Status = activeTeamStatus }
                    );
            }

            // Add HR Group Teams...
            Entities.Group CorporateCommunicationsGroup = new Entities.Group();
            CorporateCommunicationsGroup = context.GroupSet.Where(g => g.Name == "HR and Corporate Affairs").FirstOrDefault();

            if (CorporateCommunicationsGroup != null && activeTeamStatus != null)
            {
                context.TeamSet.AddOrUpdate(
                    ts => ts.Name,
                    new Entities.Team { Name = "Corporate Communications", GroupID = CorporateCommunicationsGroup.ID, Group = CorporateCommunicationsGroup, Code = "Corporate Communications", CreatedBy = 1, CreatedDate = dateToday, TeamStatusID = activeTeamStatus.ID, Status = activeTeamStatus}    
                );
            }

            List<Entities.Permission> TeamManagerPermissions = context.PermissionSet.Where(p => p.isTeamLevel == true).ToList();
            List<Entities.Permission> TeamOtherPermissions = context.PermissionSet.Where(p => p.Name == "READ_TEAM_PROJECT" || p.Name == "READ_GROUP_PROJECT").ToList();

            // Add Team Roles...
            context.TeamRoleSet.AddOrUpdate(
                tr => tr.Name,
                new Entities.TeamRole { Name = "TeamManager", CreatedBy = 1, CreatedDate = dateToday, DefaultPermissions = TeamManagerPermissions, isSystemGenerated = true },
                new Entities.TeamRole { Name = "TeamMember", CreatedBy = 1, CreatedDate = dateToday, DefaultPermissions = TeamOtherPermissions, isSystemGenerated = true },
                new Entities.TeamRole { Name = "TeamAdmin", CreatedBy = 1, CreatedDate = dateToday, DefaultPermissions = TeamOtherPermissions, isSystemGenerated = true }
                );

            List<Entities.Permission> ScrumProjectAdminPermissions = context.PermissionSet.Where(p => p.isProjectLevel == true).ToList();
            List<Entities.Permission> ScrumProjectOtherPermissions = context.PermissionSet.Where(p => p.Name == "READ_TEAM_PROJECT" || p.Name == "READ_GROUP_PROJECT").ToList();

            // Add scrum roles
            context.ScrumRoleSet.AddOrUpdate(
               sr => sr.Name,
               new Entities.ScrumRole { Name = "Product Owner", CreatedBy = 1, CreatedDate = dateToday, DefaultPermissions = ScrumProjectOtherPermissions, isSystemGenerated = true },
               new Entities.ScrumRole { Name = "Team Member", CreatedBy = 1, CreatedDate = dateToday, DefaultPermissions = ScrumProjectOtherPermissions, isSystemGenerated = true },
               new Entities.ScrumRole { Name = "Scrum Master", CreatedBy = 1, CreatedDate = dateToday, DefaultPermissions = ScrumProjectOtherPermissions, isSystemGenerated = true },
               new Entities.ScrumRole { Name = "Project Admin", CreatedBy = 1, CreatedDate = dateToday, DefaultPermissions = ScrumProjectAdminPermissions, isSystemGenerated = true }
            );

            // Add user status
            context.UserStatusSet.AddOrUpdate(
               us => us.Name,
               new Entities.UserStatus { Name = "Active", CreatedBy = 1, CreatedDate = dateToday },
               new Entities.UserStatus { Name = "Inactive", CreatedBy = 1, CreatedDate = dateToday },
               new Entities.UserStatus { Name = "Deleted", CreatedBy = 1, CreatedDate = dateToday }
            );

            // Add member status
            context.MemberStatusSet.AddOrUpdate(
              ms => ms.Name,
              new Entities.MemberStatus { Name = "Active", CreatedBy = 1, CreatedDate = dateToday },
              new Entities.MemberStatus { Name = "Inactive", CreatedBy = 1, CreatedDate = dateToday }
            );

            // Project schedule status
            context.ProjectScheduleStatusSet.AddOrUpdate(
              pss => pss.Name,
              new Entities.ProjectScheduleStatus { Name = "Active", CreatedBy = 1, CreatedDate = dateToday },
              new Entities.ProjectScheduleStatus { Name = "Inactive", CreatedBy = 1, CreatedDate = dateToday },
              new Entities.ProjectScheduleStatus { Name = "Deleted", CreatedBy = 1, CreatedDate = dateToday }
            );

            // Project schedule activity status
            context.ProjectScheduleActivityStatus.AddOrUpdate(
             psas => psas.Name,
             new Entities.ProjectScheduleActivityStatus { Name = "Pending", CreatedBy = 1, CreatedDate = dateToday },
             new Entities.ProjectScheduleActivityStatus { Name = "On-Going", CreatedBy = 1, CreatedDate = dateToday },
             new Entities.ProjectScheduleActivityStatus { Name = "Completed", CreatedBy = 1, CreatedDate = dateToday }
             );

            // Project status
            context.ProjectStatusSet.AddOrUpdate(
            ps => ps.Name,
            new Entities.ProjectStatus { Name = "Active", CreatedBy = 1, CreatedDate = dateToday },
            new Entities.ProjectStatus { Name = "Inactive", CreatedBy = 1, CreatedDate = dateToday },
            new Entities.ProjectStatus { Name = "Deleted", CreatedBy = 1, CreatedDate = dateToday }
            );

            // Project category
            context.ProjectCategorySet.AddOrUpdate(
              pc => pc.Name,
              new Entities.ProjectCategory { Name = "Application Implementation", CreatedBy = 1, CreatedDate = dateToday },
              new Entities.ProjectCategory { Name = "Integration", CreatedBy = 1, CreatedDate = dateToday },
              new Entities.ProjectCategory { Name = "Test", CreatedBy = 1, CreatedDate = dateToday },
              new Entities.ProjectCategory { Name = "Upgrade", CreatedBy = 1, CreatedDate = dateToday },
              new Entities.ProjectCategory { Name = "Infrastructure", CreatedBy = 1, CreatedDate = dateToday },
              new Entities.ProjectCategory { Name = "Improvement", CreatedBy = 1, CreatedDate = dateToday }
            );

            // Task status
            context.TaskStatusSet.AddOrUpdate(
              ts => ts.Name,
              new Entities.TaskStatus { Name = "ToDo", Description = "Task is still in the backlog and no work has been done yet.", CreatedBy = 1, CreatedDate = dateToday },
              new Entities.TaskStatus { Name = "InProgress", Description = "Task has been assigned and work has started.", CreatedBy = 1, CreatedDate = dateToday },
              new Entities.TaskStatus { Name = "Verification", Description = "Task is being verified or is being tested.", CreatedBy = 1, CreatedDate = dateToday },
              new Entities.TaskStatus { Name = "Done", Description = "Task is done or completed.", CreatedBy = 1, CreatedDate = dateToday }

            );

            // Backlog Item Status
            context.BacklogItemStatusSet.AddOrUpdate(
               bis => bis.Name,
               new Entities.BacklogItemStatus { Name = "New", Description = "Backlog item is newly created.", CreatedBy = 1, CreatedDate = dateToday },
               new Entities.BacklogItemStatus { Name = "Approved", Description = "Backlog item is approved and can now be seen by the team.", CreatedBy = 1, CreatedDate = dateToday }
            );

            // Backlog Item Priority
            context.BacklogItemPrioritySet.AddOrUpdate(
               bip => bip.Name,
               new Entities.BacklogItemPriority { Name = "Must Have", Description = "The requirement is essential, key stakeholder needs will not be satisfied if this requirement is not delivered and the timebox will be considered to have failed. MUST can be considered a backronum from Minimum Usable SubseT", CreatedBy = 1, CreatedDate = dateToday },
               new Entities.BacklogItemPriority { Name = "Should Have", Description = "This is an important requirement but if it is not delivered within the current timebox, there is an acceptable workaround until it is delivered during a subsequent timebox", CreatedBy = 1, CreatedDate = dateToday },
               new Entities.BacklogItemPriority { Name = "Could Have", Description = "This is a nice to have requirement; we have estimated that it is possible to deliver this in the given time but will be one of the requirements de-scoped if we have underestimated", CreatedBy = 1, CreatedDate = dateToday },
               new Entities.BacklogItemPriority { Name = "Won't Have", Description = "The full name of this category is ‘Would like to have but Won’t Have during this timebox’; requirements in this category will not be delivered within the timebox that the prioritisation applies to", CreatedBy = 1, CreatedDate = dateToday }
            );
        }
    }
}
