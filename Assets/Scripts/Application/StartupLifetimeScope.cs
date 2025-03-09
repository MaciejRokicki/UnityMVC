using MVC.Logic;
using VContainer;
using VContainer.Unity;

namespace MVC.Application
{
    public class StartupLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Startup>();
        }
    }
}
