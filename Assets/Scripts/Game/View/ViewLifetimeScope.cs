using MVC.Game.View.MonoBehaviorus;
using MVC.Game.View.Services.Enemy;
using MVC.Game.View.Services.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MVC.Game.View
{
    public class ViewLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private PlayerHealthVMB playerHealthView;

        [SerializeField]
        private Transform enemyHealthContainer;
        [SerializeField]
        private EnemyHealthVMB enemyHealthViewPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterPlayer(builder);
            RegisterEnemy(builder);
        }

        private void RegisterPlayer(IContainerBuilder builder)
        {
            builder.RegisterComponent(playerHealthView);

            builder.RegisterEntryPoint<PlayerHealthViewService>();
        }

        private void RegisterEnemy(IContainerBuilder builder)
        {
            builder.RegisterComponent(enemyHealthContainer);
            builder.RegisterComponent(enemyHealthViewPrefab);

            builder.RegisterEntryPoint<EnemyHealthViewService>();
        }
    }
}
