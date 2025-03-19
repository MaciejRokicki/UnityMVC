using MVC.Game.Logic.Interfaces.Enemy;
using MVC.Game.Logic.Interfaces.Player;
using MVC.Game.Logic.MonoBehaviours;
using VContainer.Unity;

namespace MVC.Game.Logic.Services.Enemy
{
    public class EnemyMovementService : IEnemyMovementService, ITickable
    {
        private readonly IEnemyService enemyService;
        private readonly IPlayerService playerService;

        public EnemyMovementService(IEnemyService enemyService, IPlayerService playerService)
        {
            this.enemyService = enemyService;
            this.playerService = playerService;
        }

        public void Tick()
        {
            for (int i = enemyService.Enemies.Count - 1; i > -1; i--)
            {
                EnemyMB enemy = enemyService.Enemies[i];
                enemy.NavMeshAgent.SetDestination(playerService.Player.transform.position);
            }
        }
    }
}