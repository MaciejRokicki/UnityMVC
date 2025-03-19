using MVC.Common;
using MVC.Game.Logic.Interfaces.Enemy;
using MVC.Game.Logic.Interfaces.Player;
using MVC.Game.Logic.MonoBehaviours;
using MVC.Game.Logic.ScriptableObjects;
using MVC.Game.Model.Player;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Game.Logic.Services.Player
{
    public class PlayerCombatService : IPlayerCombatService, IInitializable
    {
        private readonly IPlayerService playerService;
        private readonly IEnemyService enemyService;
        private readonly IEnemyHealthService enemyHealthService;

        private readonly PlayerCombatSettings combatSettings;

        private PlayerCombatModel combatData;
        private Subject<ChangedValue<float>> onDamageChanged;

        public PlayerCombatModel CombatData => combatData;

        public Subject<ChangedValue<float>> OnDamageChanged => onDamageChanged;

        public PlayerCombatService(
            IPlayerService playerService,
            IEnemyService enemyService,
            IEnemyHealthService enemyHealthService,
            PlayerCombatSettings combatSettings)
        {
            this.playerService = playerService;
            this.enemyService = enemyService;
            this.enemyHealthService = enemyHealthService;
            this.combatSettings = combatSettings;
        }

        public void Initialize()
        {
            combatData = new PlayerCombatModel();
            combatData.Damage = combatSettings.Damage;

            onDamageChanged = new Subject<ChangedValue<float>>();
        }

        public void ChangeDamage(float damage)
        {
            if (combatData.Damage == damage)
                return;

            float previousDamage = combatData.Damage;
            combatData.Damage = damage;
            OnDamageChanged.OnNext(new ChangedValue<float>(previousDamage, combatData.Damage));
        }

        public void Attack()
        {
            for (int i = enemyService.Enemies.Count - 1; i > -1; i--)
            {
                EnemyMB enemy = enemyService.Enemies[i];

                if (Vector3.Distance(playerService.Player.transform.position, enemy.transform.position) < combatSettings.MinDistance)
                {
                    enemyHealthService.Hit(enemy, combatData.Damage);
                }
            }
        }
    }
}