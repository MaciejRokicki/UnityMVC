using MVC.Game.Logic.Interfaces.Enemy;
using MVC.Game.Logic.MonoBehaviours;

namespace MVC.Game.Logic.Services.Enemy
{
    public class EnemyHealthService : IEnemyHealthService
    {
        private readonly IEnemyService enemyService;

        public EnemyHealthService(IEnemyService enemyService)
        {
            this.enemyService = enemyService;
        }

        public void Hit(EnemyMB enemy, float damage)
        {
            float previousValue = enemy.Health.Health;
            enemy.Health.IncreaseHealth(-damage);

            if (previousValue > 0.0f && enemy.Health.Health == 0.0f)
            {
                Kill(enemy);
            }
        }

        public void Kill(EnemyMB enemy)
        {
            enemyService.Release(enemy);
        }
    }
}