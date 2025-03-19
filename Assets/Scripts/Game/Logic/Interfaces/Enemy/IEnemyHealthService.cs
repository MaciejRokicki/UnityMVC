using MVC.Game.Logic.MonoBehaviours;

namespace MVC.Game.Logic.Interfaces.Enemy
{
    public interface IEnemyHealthService
    {
        public void Hit(EnemyMB enemy, float damage);
        public void Kill(EnemyMB enemy);
    }
}