using MVC.ViewLogic.Services.Enemy;
using MVC.ViewLogic.Services.Player;
using MVC.ViewModels;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MVC.Application
{
    public class ViewLogicLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private PlayerHealthViewModel playerHealthViewModel;

        [SerializeField]
        private Transform enemyHealthContainer;
        [SerializeField]
        private EnemyHealthViewModel enemyHealthViewModel;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterPlayer(builder);
            RegisterEnemy(builder);
        }

        private void RegisterPlayer(IContainerBuilder builder)
        {
            builder.RegisterComponent(playerHealthViewModel);

            builder.RegisterEntryPoint<PlayerHealthViewService>();
        }

        private void RegisterEnemy(IContainerBuilder builder)
        {
            builder.RegisterComponent(enemyHealthContainer);
            builder.RegisterComponent(enemyHealthViewModel);

            builder.RegisterEntryPoint<EnemyHealthViewService>();
        }
    }
}
