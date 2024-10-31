using Autofac;
using Inventory_Management.Common;
using Inventory_Management.Common.Repositories;
using Inventory_Management.Data;
using Inventory_Management.Entities;

namespace Common
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Context>().InstancePerLifetimeScope();
            //builder.RegisterGeneric(typeof(RequestParameters<>)).InstancePerLifetimeScope();
            builder.RegisterType<UserState>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(RequestParameters<>).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<RequestParameters<User>>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RequestParameters<Role>>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RequestParameters<OtpVerification>>().AsSelf().InstancePerLifetimeScope();
            //marwa.asm95@gmail.com
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

        }
    }
}
