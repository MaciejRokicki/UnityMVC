using MVC.Core.Data.Enemy;
using MVC.Core.Logic.Interfaces.Enemy;
using MVC.Core.Logic.Interfaces.Player;
using VContainer.Unity;

namespace MVC.Core.Logic.Services.Enemy
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
                EnemyData enemy = enemyService.Enemies[i];
                enemy.Model.NavMeshAgent.SetDestination(playerService.PlayerData.Model.transform.position);
            }
        }
    }
}