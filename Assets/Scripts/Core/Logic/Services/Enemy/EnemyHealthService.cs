using MVC.Core.Data.Enemy;
using MVC.Core.Logic.Interfaces.Enemy;

namespace MVC.Core.Logic.Services.Enemy
{
    public class EnemyHealthService : IEnemyHealthService
    {
        private readonly IEnemyService enemyService;

        public EnemyHealthService(IEnemyService enemyService)
        {
            this.enemyService = enemyService;
        }

        public void Hit(EnemyData enemyData, float damage)
        {
            float previousValue = enemyData.HealthData.Health;
            enemyData.HealthData.IncreaseHealth(-damage);

            if (previousValue > 0.0f && enemyData.HealthData.Health == 0.0f)
            {
                Kill(enemyData);
            }
        }

        public void Kill(EnemyData enemyData)
        {
            enemyService.Release(enemyData);
        }
    }
}