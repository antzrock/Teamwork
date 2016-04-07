using Autofac;
using Autofac.Integration.WebApi;
using SysAid.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Weekly.Data;
using Weekly.Data.Infrastructure;
using Weekly.Data.Repositiories;
using Weekly.Services;
using Weekly.Services.Abstract;

namespace WEEKLY.Web.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // EF Weekly Context
            builder.RegisterType<TeamworkContext>()
                   .As<DbContext>()
                   .InstancePerRequest();

            builder.RegisterType<DbFactory>()
                .As<IDbFactory>()
                .InstancePerRequest();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(EntityBaseRepository<>))
                   .As(typeof(IEntityBaseRepository<>))
                   .InstancePerRequest();

            // Services
            builder.RegisterType<EncryptionService>()
                .As<IEncryptionService>()
                .InstancePerRequest();

            builder.RegisterType<MembershipService>()
                .As<IMembershipService>()
                .InstancePerRequest();

            builder.RegisterType<ActiveDirectoryService>()
                .As<IActiveDirectoryService>()
                .InstancePerRequest();

            builder.RegisterType<WeekNumberService>()
                .As<IWeekNumberService>()
                .InstancePerRequest();

            builder.RegisterType<EmailService>()
                .As<IEmailService>()
                .InstancePerRequest();

            builder.RegisterType<ServiceDeskRepository>()
                .As<IServiceDeskRepository>()
                .InstancePerRequest();

            builder.RegisterType<ServiceDeskService>()
                .As<IServiceDeskService>()
                .InstancePerRequest();

            builder.RegisterType<ProjectService>()
                .As<IProjectService>()
                .InstancePerRequest();

            builder.RegisterType<WeeklyReportService>()
                .As<IWeeklyReportService>()
                .InstancePerRequest();

            builder.RegisterType<DateTimeService>()
                .As<IDateTimeService>()
                .InstancePerRequest();

            builder.RegisterType<DataSetService>()
                .As<IDataSetService>()
                .InstancePerRequest();

            builder.RegisterType<GroupService>()
                .As<IGroupService>()
                .InstancePerRequest();

            builder.RegisterType<TeamService>()
                .As<ITeamService>()
                .InstancePerRequest();

            Container = builder.Build();

            return Container;
        }
    }
}