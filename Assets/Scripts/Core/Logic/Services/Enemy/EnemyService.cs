using Cysharp.Threading.Tasks;
using MVC.Core.Data.Enemy;
using MVC.Core.Logic.Interfaces.Enemy;
using MVC.Core.Models;
using MVC.Core.Models.Settings;
using R3;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using VContainer.Unity;

namespace MVC.Core.Logic.Services.Enemy
{
    public class EnemyService : IEnemyService, IInitializable, IStartable
    {
        private readonly EnemyGeneralSettings generalSettings;
        private readonly EnemyHealthSettings healthSettings;
        private readonly EnemyMovementSettings movementSettings;
        private readonly EnemyCombatSettings combatSettings;
        private readonly EnemyModel model;

        private IObjectPool<EnemyData> enemyPool;
        private List<EnemyData> enemies = new List<EnemyData>();

        private Subject<EnemyData> onEnemySpawned;
        private Subject<EnemyData> onEnemyDestroyed;

        private CancellationTokenSource respawnEnemiesCts;

        public List<EnemyData> Enemies => enemies;

        public Subject<EnemyData> OnEnemySpawned => onEnemySpawned;
        public Subject<EnemyData> OnEnemyDestroyed => onEnemyDestroyed;

        public EnemyService(
            EnemyGeneralSettings generalSettings,
            EnemyHealthSettings healthSettings,
            EnemyMovementSettings movementSettings,
            EnemyCombatSettings combatSettings,
            EnemyModel model)
        {
            this.generalSettings = generalSettings;
            this.healthSettings = healthSettings;
            this.movementSettings = movementSettings;
            this.combatSettings = combatSettings;
            this.model = model;
        }

        public void Initialize()
        {
            onEnemySpawned = new Subject<EnemyData>();
            onEnemyDestroyed = new Subject<EnemyData>();

            enemyPool = new ObjectPool<EnemyData>(
                () =>
                {
                    EnemyData enemyData = new EnemyData();
                    enemyData.Model = GameObject.Instantiate(model);
                    enemyData.HealthData = new EnemyHealthData(healthSettings.MaxHealth);
                    enemyData.MovementData = new EnemyMovementData();
                    enemyData.CombatData = new EnemyCombatData();

                    return enemyData;
                }, (EnemyData enemyData) =>
                {
                    enemyData.HealthData.IncreaseHealth(healthSettings.MaxHealth);
                    enemyData.MovementData.Speed = movementSettings.Speed;
                    enemyData.CombatData.AttackSpeed = combatSettings.AttackSpeed;
                    enemyData.CombatData.Damage = combatSettings.Damage;
                    enemyData.CombatData.HitTimer = 0.0f;
                    enemyData.Model.gameObject.SetActive(true);
                    enemies.Add(enemyData);

                    onEnemySpawned.OnNext(enemyData);
                }, (EnemyData enemyData) =>
                {
                    onEnemyDestroyed.OnNext(enemyData);

                    enemyData.Model.gameObject.SetActive(false);
                    enemies.Remove(enemyData);

                    if (!respawnEnemiesCts.IsCancellationRequested)
                    {
                        respawnEnemiesCts.Cancel();
                        respawnEnemiesCts.Dispose();
                        respawnEnemiesCts = new CancellationTokenSource();
                    }

                    RespawnEnemiesAsync(respawnEnemiesCts).Forget();
                }, (EnemyData enemyData) =>
                {
                    GameObject.Destroy(enemyData.Model.gameObject);
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

        public EnemyData Spawn()
        {
            NavMeshHit hit;
            Vector3 randomPosition = new Vector3(
                Random.Range(-10.0f, 10.0f),
                1.0f,
                Random.Range(-10.0f, 10.0f));

            while (!NavMesh.SamplePosition(randomPosition, out hit, 2.0f, NavMesh.AllAreas)) { }

            EnemyData enemyData = enemyPool.Get();
            enemyData.Model.transform.position = hit.position;

            return enemyData;
        }

        public void Release(EnemyData enemyData)
        {
            enemyPool.Release(enemyData);
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