using MVC.Core.Data.Enemy;

namespace MVC.Core.Logic.Interfaces.Enemy
{
    public interface IEnemyCombatService
    {
        public void HitPlayer(EnemyData enemyData);
    }
}