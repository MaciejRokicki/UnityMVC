using MVC.Common;
using MVC.Core.Data.Enemy;
using MVC.Core.Data.Player;
using MVC.Core.Logic.Interfaces.Enemy;
using MVC.Core.Logic.Interfaces.Player;
using MVC.Core.Logic.Services.Player.CombatAbility;
using MVC.Core.Models.Settings;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Core.Logic.Services.Player
{
    public class PlayerCombatService : IPlayerCombatService, IInitializable
    {
        private readonly IPlayerService playerService;
        private readonly IEnemyService enemyService;
        private readonly IEnemyHealthService enemyHealthService;

        private readonly PlayerCombatSettings combatSettings;

        private PlayerCombatData combatData;
        private Subject<ChangedValue<float>> onDamageChanged;

        public PlayerCombatData CombatData => combatData;

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
            combatData = new PlayerCombatData();
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
                EnemyData enemy = enemyService.Enemies[i];

                if (Vector3.Distance(playerService.PlayerData.Model.transform.position, enemy.Model.transform.position) < combatSettings.MinDistance)
                {
                    enemyHealthService.Hit(enemy, combatData.Damage);
                }
            }
        }
    }
}