using MVC.Core.Data.Enemy;
using R3;
using System.Collections.Generic;

namespace MVC.Core.Logic.Interfaces.Enemy
{
    public interface IEnemyService
    {
        public Subject<EnemyData> OnEnemySpawned { get; }
        public Subject<EnemyData> OnEnemyDestroyed { get; }

        public List<EnemyData> Enemies { get; }

        public EnemyData Spawn();
        public void Release(EnemyData enemyData);
    }
}