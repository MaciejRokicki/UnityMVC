using MVC.Core.Logic;
using VContainer;
using VContainer.Unity;

namespace MVC.Core.Application.LifetimeScopes
{
    public class StartupLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Startup>();
        }
    }
}
