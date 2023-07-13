using Autofac;
using HPASS.AutoMapping.Abstraction;
using HPASS.AutoMapping.Implementation;
using HPASS.Business.Main.Abstraction;
using HPASS.Business.Main.Implementation;
using HPASS.Crosscutting.Abstraction;
using HPASS.Crosscutting.Implementation;
using HPASS.DataAccessLayer.Abstraction;
using HPASS.DataAccessLayer.Implementation;
using HPASS.EfCore.Context;

namespace HPASS.Service.IocBuilder
{
    public class DependencyInjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(HpassDbContext)).InstancePerLifetimeScope();

            builder.RegisterType(typeof(MappingManager)).As(typeof(IMappingManager)).SingleInstance();


            builder.RegisterType(typeof(UserSessionContext)).As(typeof(IUserSessionContext)).InstancePerLifetimeScope();

            builder.RegisterType(typeof(MainBusiness)).As(typeof(IMainBusiness));


            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

        }
    }
}
