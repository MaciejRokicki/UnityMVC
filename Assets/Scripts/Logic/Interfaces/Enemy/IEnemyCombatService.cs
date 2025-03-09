using MVC.Data.Enemy;

namespace MVC.Logic.Interfaces.Enemy
{
    public interface IEnemyCombatService
    {
        public void HitPlayer(EnemyData enemyData);
    }
}