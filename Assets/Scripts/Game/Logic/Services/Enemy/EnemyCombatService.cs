using MVC.Game.Logic.Interfaces.Enemy;
using MVC.Game.Logic.Interfaces.Player;
using MVC.Game.Logic.MonoBehaviours;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Game.Logic.Services.Enemy
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
                EnemyMB enemy = enemyService.Enemies[i];

                if (enemy.NavMeshAgent.remainingDistance < 1.0f)
                {
                    enemy.Combat.HitTimer += Time.deltaTime;

                    if (enemy.Combat.HitTimer > enemy.Combat.AttackSpeed)
                    {
                        HitPlayer(enemy);
                        enemy.Combat.HitTimer = 0.0f;
                    }
                }
                else
                {
                    enemy.Combat.HitTimer = 0.0f;
                }
            }
        }

        public void HitPlayer(EnemyMB enemy)
        {
            playerHealthService.IncreaseHealth(-enemy.Combat.Damage);
        }
    }
}