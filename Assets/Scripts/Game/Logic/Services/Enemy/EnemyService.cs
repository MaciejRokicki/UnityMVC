using Cysharp.Threading.Tasks;
using MVC.Game.Logic.Interfaces.Enemy;
using MVC.Game.Logic.MonoBehaviours;
using MVC.Game.Logic.ScriptableObjects;
using MVC.Game.Model.Enemy;
using R3;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace MVC.Game.Logic.Services.Enemy
{
    public class EnemyService : IEnemyService, IInitializable, IStartable
    {
        private readonly EnemyGeneralSettings generalSettings;
        private readonly EnemyHealthSettings healthSettings;
        private readonly EnemyMovementSettings movementSettings;
        private readonly EnemyCombatSettings combatSettings;
        private readonly EnemyMB enemyPrefab;
        private readonly IObjectResolver objectResolver;

        private IObjectPool<EnemyMB> enemyPool;
        private List<EnemyMB> enemies = new List<EnemyMB>();

        private Subject<EnemyMB> onEnemySpawned;
        private Subject<EnemyMB> onEnemyDestroyed;

        private CancellationTokenSource respawnEnemiesCts;

        public List<EnemyMB> Enemies => enemies;

        public Subject<EnemyMB> OnEnemySpawned => onEnemySpawned;
        public Subject<EnemyMB> OnEnemyDestroyed => onEnemyDestroyed;

        public EnemyService(
            EnemyGeneralSettings generalSettings,
            EnemyHealthSettings healthSettings,
            EnemyMovementSettings movementSettings,
            EnemyCombatSettings combatSettings,
            EnemyMB enemyPrefab,
            IObjectResolver objectResolver)
        {
            this.generalSettings = generalSettings;
            this.healthSettings = healthSettings;
            this.movementSettings = movementSettings;
            this.combatSettings = combatSettings;
            this.enemyPrefab = enemyPrefab;
            this.objectResolver = objectResolver;
        }

        public void Initialize()
        {
            onEnemySpawned = new Subject<EnemyMB>();
            onEnemyDestroyed = new Subject<EnemyMB>();

            enemyPool = new ObjectPool<EnemyMB>(
                () =>
                {
                    EnemyMB enemy = objectResolver.Instantiate(enemyPrefab);
                    enemy.Health = new EnemyHealthModel(healthSettings.MaxHealth);
                    enemy.Movement = new EnemyMovementModel();
                    enemy.Combat = new EnemyCombatModel();

                    return enemy;
                }, (EnemyMB enemy) =>
                {
                    enemy.Health.IncreaseHealth(healthSettings.MaxHealth);
                    enemy.Movement.Speed = movementSettings.Speed;
                    enemy.Combat.AttackSpeed = combatSettings.AttackSpeed;
                    enemy.Combat.Damage = combatSettings.Damage;
                    enemy.Combat.HitTimer = 0.0f;
                    enemy.gameObject.SetActive(true);
                    enemies.Add(enemy);

                    onEnemySpawned.OnNext(enemy);
                }, (EnemyMB enemy) =>
                {
                    onEnemyDestroyed.OnNext(enemy);

                    enemy.gameObject.SetActive(false);
                    enemies.Remove(enemy);

                    if (!respawnEnemiesCts.IsCancellationRequested)
                    {
                        respawnEnemiesCts.Cancel();
                        respawnEnemiesCts.Dispose();
                        respawnEnemiesCts = new CancellationTokenSource();
                    }

                    RespawnEnemiesAsync(respawnEnemiesCts).Forget();
                }, (EnemyMB enemy) =>
                {
                    GameObject.Destroy(enemy.gameObject);
                });

            respawnEnemiesCts = new CancellationTokenSource();
        }

        public void Start()
        {
            for (int i = 0; i < generalSettings.Count; i++)
            {
                Spawn();
            }
        }

        public EnemyMB Spawn()
        {
            NavMeshHit hit;
            Vector3 randomPosition = new Vector3(
                Random.Range(-10.0f, 10.0f),
                1.0f,
                Random.Range(-10.0f, 10.0f));

            while (!NavMesh.SamplePosition(randomPosition, out hit, 2.0f, NavMesh.AllAreas)) { }

            EnemyMB enemy = enemyPool.Get();
            enemy.transform.position = hit.position;

            return enemy;
        }

        public void Release(EnemyMB enemy)
        {
            enemyPool.Release(enemy);
        }

        private async UniTaskVoid RespawnEnemiesAsync(CancellationTokenSource cts)
        {
            float timer = 0.0f;

            do
            {
                if (cts.IsCancellationRequested)
                    break;

                timer += Time.deltaTime;

                if (timer > generalSettings.RespawnTime)
                {
                    timer = 0.0f;
                    Spawn();
                }

                await UniTask.NextFrame();
            } while (enemies.Count < generalSettings.Count);
        }
    }
}