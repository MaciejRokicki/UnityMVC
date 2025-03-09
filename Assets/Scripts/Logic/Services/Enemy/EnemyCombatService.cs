using MVC.Data.Enemy;
using MVC.Logic.Interfaces.Enemy;
using MVC.Logic.Interfaces.Player;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Logic.Services.Enemy
{
    public class EnemyCombatService : IEnemyCombatService, ITickable
    {
        private readonly IEnemyService enemyService;
        private readonly IPlayerHealthService playerHealthService;

        public EnemyCombatService(
            IEnemyService enemyService,
            IPlayerHealthService playerHealthService)
        {
            this.enemyService = enemyService;
            this.playerHealthService = playerHealthService;
        }

        public void Tick()
        {
            for (int i = enemyService.Enemies.Count - 1; i > -1; i--)
            {
                EnemyData enemy = enemyService.Enemies[i];

                if (enemy.Model.NavMeshAgent.remainingDistance < 1.0f)
                {
                    enemy.CombatData.HitTimer += Time.deltaTime;

                    if (enemy.CombatData.HitTimer > enemy.CombatData.AttackSpeed)
                    {
                        HitPlayer(enemy);
                        enemy.CombatData.HitTimer = 0.0f;
                    }
                }
                else
                {
                    enemy.CombatData.HitTimer = 0.0f;
                }
            }
        }

        public void HitPlayer(EnemyData enemyData)
        {
            playerHealthService.IncreaseHealth(-enemyData.CombatData.Damage);
        }
    }
}