using Autofac;
using backapitest.Controllers;
using System.Reflection;

namespace backapitest.DependencyInjection
{
    public class AutoFacConfiguration : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);


            builder.RegisterType<SupabaseService>().As<ISupabaseService>().InstancePerDependency();
        }
    }
}
