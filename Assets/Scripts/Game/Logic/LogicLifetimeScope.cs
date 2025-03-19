using MVC.Game.Logic.Input;
using MVC.Game.Logic.MonoBehaviours;
using MVC.Game.Logic.ScriptableObjects;
using MVC.Game.Logic.Services;
using MVC.Game.Logic.Services.Enemy;
using MVC.Game.Logic.Services.Player;
using MVC.Game.Logic.Services.Player.CombatAbility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MVC.Game.Logic
{
    public class LogicLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private PlayerSettingsSO playerSettings;
        [SerializeField]
        private EnemySettingsSO enemySettings;

        [SerializeField]
        private PlayerMB playerPrefab;
        [SerializeField]
        private EnemyMB enemyPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterPlayer(builder);
            RegisterEnemy(builder);
            RegisterOther(builder);
        }

        private void RegisterPlayer(IContainerBuilder builder)
        {
            builder.RegisterComponent(playerPrefab);

            builder.RegisterInstance(playerSettings.Health);
            builder.RegisterInstance(playerSettings.Movement);
            builder.RegisterInstance(playerSettings.Combat);

            builder.Register<PlayerCombatAbility1Factory>(Lifetime.Singleton);
            builder.Register<PlayerCombatAbility2Factory>(Lifetime.Singleton);

            builder.RegisterEntryPoint<PlayerService>();
            builder.RegisterEntryPoint<PlayerHealthService>();
            builder.RegisterEntryPoint<PlayerMovementService>();
            builder.RegisterEntryPoint<PlayerCombatService>();
            builder.RegisterEntryPoint<PlayerAbilityService>();
        }

        private void RegisterEnemy(IContainerBuilder builder)
        {
            builder.RegisterComponent(enemyPrefab);

            builder.RegisterInstance(enemySettings.General);
            builder.RegisterInstance(enemySettings.Health);
            builder.RegisterInstance(enemySettings.Movement);
            builder.RegisterInstance(enemySettings.Combat);

            builder.RegisterEntryPoint<EnemyService>();
            builder.RegisterEntryPoint<EnemyHealthService>();
            builder.RegisterEntryPoint<EnemyMovementService>();
            builder.RegisterEntryPoint<EnemyCombatService>();
        }

        private void RegisterOther(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<InputService>();
        }
    }
}
