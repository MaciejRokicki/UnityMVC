using MVC.Game.Logic.MonoBehaviours;

namespace MVC.Game.Logic.Interfaces.Enemy
{
    public interface IEnemyCombatService
    {
        public void HitPlayer(EnemyMB enemy);
    }
}