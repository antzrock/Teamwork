using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weekly.Entities;
using WEEKLY.Web.Models;

namespace WEEKLY.Web.Infrastructure.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappingProfile"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Group, GroupViewModel>()
                .ForMember(vm => vm.GroupID, map => map.MapFrom(m => m.ID))
                .ForMember(vm => vm.Name, map => map.MapFrom(m=> m.Name))
                .ForMember(vm => vm.Code, map => map.MapFrom(m => m.Code));

            Mapper.CreateMap<Team, TeamViewModel>()
                .ForMember(vm => vm.TeamID, map => map.MapFrom(m => m.ID))
                .ForMember(vm => vm.Name, map => map.MapFrom(m => m.Name))
                .ForMember(vm => vm.Code, map => map.MapFrom(m => m.Code));

            Mapper.CreateMap<User, UserViewModel>()
                .ForMember(vm => vm.UserID, map => map.MapFrom(m => m.ID))
                .ForMember(vm => vm.Username, map => map.MapFrom(m => m.Username))
                .ForMember(vm => vm.Email, map => map.MapFrom(m => m.Email))
                .ForMember(vm => vm.Fullname, map => map.MapFrom(m => m.Fullname))
                .ForMember(vm => vm.AvatarPicPath, map => map.MapFrom(m => m.AvatarPicPath))
                .ForMember(vm => vm.ProfilePicPath, map => map.MapFrom(m => m.ProfilePicPath))
                .ForMember(vm => vm.ProfileQuote, map => map.MapFrom(m => m.ProfileQuote))
                .ForMember(vm => vm.Nickname, map => map.MapFrom(m => m.Nickname))
                .ForMember(vm => vm.Title, map => map.MapFrom(m => m.Title));

            Mapper.CreateMap<ScrumRole, ScrumRoleViewModel>()
                .ForMember(vm => vm.ScrumRoleID, map => map.MapFrom(m => m.ID))
                .ForMember(vm => vm.Rolename, map => map.MapFrom(m => m.Name));

            Mapper.CreateMap<Project, ProjectViewModel>()
                .ForMember(vm => vm.ProjectID, map => map.MapFrom(m => m.ID))
                .ForMember(vm => vm.Name, map => map.MapFrom(m => m.Name))
                .ForMember(vm => vm.Code, map => map.MapFrom(m => m.Code))
                .ForMember(vm => vm.LogoPath, map => map.MapFrom(m => m.LogoPath));

            Mapper.CreateMap<ProjectStatus, ProjectStatusViewModel>()
                .ForMember(vm => vm.ProjectStatusID, map => map.MapFrom(m => m.ID))
                .ForMember(vm => vm.Name, map => map.MapFrom(m => m.Name));

            Mapper.CreateMap<MemberStatus, MemberStatusViewModel>()
                .ForMember(vm => vm.MemberStatusID, map => map.MapFrom(m => m.ID))
                .ForMember(vm => vm.Status, map => map.MapFrom(m => m.Name));

            Mapper.CreateMap<ProjectScheduleStatus, ProjectScheduleStatusViewModel>()
                .ForMember(vm => vm.ProjectScheduleStatusID, map => map.MapFrom(m => m.ID))
                .ForMember(vm => vm.Status, map => map.MapFrom(m => m.Name));

            Mapper.CreateMap<ProjectScheduleActivityStatus, ProjectScheduleActivityStatusViewModel>()
                .ForMember(vm => vm.ProjectScheduleActivityStatusID, map => map.MapFrom(m => m.ID))
                .ForMember(vm => vm.ActivityStatus, map => map.MapFrom(m => m.Name));

            Mapper.CreateMap<Permission, PermissionViewModel>()
                .ForMember(vm => vm.PermissionID, map => map.MapFrom(m => m.ID))
                .ForMember(vm => vm.Name, map => map.MapFrom(m => m.Name));

            Mapper.CreateMap<ProjectCategory, ProjectCategoryViewModel>()
                .ForMember(vm => vm.ProjectCategoryID, map => map.MapFrom(m => m.ID))
                .ForMember(vm => vm.Name, map => map.MapFrom(m => m.Name));

            Mapper.CreateMap<GroupStatus, GroupStatusViewModel>()
                .ForMember(vm => vm.GroupStatusID, map => map.MapFrom(m => m.ID))
                .ForMember(vm => vm.Name, map => map.MapFrom(m => m.Name));

            Mapper.CreateMap<GroupRole, GroupRoleViewModel>()
                .ForMember(vm => vm.GroupRoleID, map => map.MapFrom(m => m.ID))
                .ForMember(vm => vm.Name, map => map.MapFrom(m => m.Name));

            Mapper.CreateMap<TeamStatus, TeamStatusViewModel>()
                .ForMember(vm => vm.TeamStatusID, map => map.MapFrom(m => m.ID))
                .ForMember(vm => vm.Name, map => map.MapFrom(m => m.Name));

            Mapper.CreateMap<TeamRole, TeamRoleViewModel>()
                .ForMember(vm => vm.TeamRoleID, map => map.MapFrom(m => m.ID))
                .ForMember(vm => vm.Name, map => map.MapFrom(m => m.Name));
        }
    }
}