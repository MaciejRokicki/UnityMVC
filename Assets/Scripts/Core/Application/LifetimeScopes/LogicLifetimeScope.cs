using MVC.Core.Logic.Input;
using MVC.Core.Logic.Services;
using MVC.Core.Logic.Services.Enemy;
using MVC.Core.Logic.Services.Player;
using MVC.Core.Logic.Services.Player.CombatAbility;
using MVC.Core.Models;
using MVC.Core.Models.Settings;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MVC.Core.Application.LifetimeScopes
{
    public class LogicLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private PlayerSettings playerSettings;
        [SerializeField]
        private EnemySettings enemySettings;

        [SerializeField]
        private PlayerModel playerModelPrefab;
        [SerializeField]
        private EnemyModel enemyModelPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterPlayer(builder);
            RegisterEnemy(builder);
            RegisterOther(builder);
        }

        private void RegisterPlayer(IContainerBuilder builder)
        {
            builder.RegisterComponent(playerModelPrefab);

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
            builder.RegisterComponent(enemyModelPrefab);

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
