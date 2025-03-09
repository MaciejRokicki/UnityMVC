using MVC.Core.Data.Enemy;

namespace MVC.Core.Logic.Interfaces.Enemy
{
    public interface IEnemyHealthService
    {
        public void Hit(EnemyData enemyData, float damage);
        public void Kill(EnemyData enemyData);
    }
}