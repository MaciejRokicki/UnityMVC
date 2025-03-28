using VContainer;
using VContainer.Unity;

namespace MVC.Game.Logic
{
    public class StartupLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Startup>();
        }
    }
}
