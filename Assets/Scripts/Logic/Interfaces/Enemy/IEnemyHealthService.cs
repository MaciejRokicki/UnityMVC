using MVC.Data.Enemy;

namespace MVC.Logic.Interfaces.Enemy
{
    public interface IEnemyHealthService
    {
        public void Hit(EnemyData enemyData, float damage);
        public void Kill(EnemyData enemyData);
    }
}