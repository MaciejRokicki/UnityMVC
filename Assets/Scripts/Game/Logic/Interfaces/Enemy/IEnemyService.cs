using MVC.Game.Logic.MonoBehaviours;
using R3;
using System.Collections.Generic;

namespace MVC.Game.Logic.Interfaces.Enemy
{
    public interface IEnemyService
    {
        public Subject<EnemyMB> OnEnemySpawned { get; }
        public Subject<EnemyMB> OnEnemyDestroyed { get; }

        public List<EnemyMB> Enemies { get; }

        public EnemyMB Spawn();
        public void Release(EnemyMB enemy);
    }
}