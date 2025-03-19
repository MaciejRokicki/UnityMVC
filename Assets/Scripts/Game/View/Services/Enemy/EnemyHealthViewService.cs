using MVC.Game.Logic.Interfaces.Enemy;
using MVC.Game.Logic.MonoBehaviours;
using MVC.Game.View.Interfaces.Enemy;
using MVC.Game.View.MonoBehaviorus;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using VContainer.Unity;

namespace MVC.Game.View.Services.Enemy
{
    public class EnemyHealthViewService : IEnemyHealthViewService, IInitializable, IStartable, IDisposable
    {
        private readonly Transform enemyHealthContainer;
        private readonly EnemyHealthVMB enemyHealthViewPrefab;

        private readonly IEnemyService enemyService;

        private List<EnemyHealthVMB> enemyHealthViewModels;
        private IObjectPool<EnemyHealthVMB> enemyHealthViewModelPool;
        private IDisposable disposable;

        public EnemyHealthViewService(
            Transform enemyHealthContainer,
            EnemyHealthVMB enemyHealthViewPrefab,
            IEnemyService enemyService)
        {
            this.enemyHealthContainer = enemyHealthContainer;
            this.enemyHealthViewPrefab = enemyHealthViewPrefab;
            this.enemyService = enemyService;
        }

        public void Initialize()
        {
            enemyHealthViewModels = new List<EnemyHealthVMB>();

            enemyHealthViewModelPool = new ObjectPool<EnemyHealthVMB>(
                () =>
                {
                    EnemyHealthVMB enemyHealthViewModel = GameObject.Instantiate(
                        enemyHealthViewPrefab, enemyHealthContainer);
                    return enemyHealthViewModel;
                }, (EnemyHealthVMB enemyHealthViewModel) =>
                {
                    enemyHealthViewModel.gameObject.SetActive(true);
                    enemyHealthViewModels.Add(enemyHealthViewModel);
                }, (EnemyHealthVMB enemyHealthViewModel) =>
                {
                    enemyHealthViewModels.Remove(enemyHealthViewModel);
                    enemyHealthViewModel.Enemy = null;
                    enemyHealthViewModel.gameObject.SetActive(false);
                }, (EnemyHealthVMB enemyHealthViewModel) =>
                {
                    GameObject.Destroy(enemyHealthViewModel.gameObject);
                });

            disposable = Disposable.Combine(
                enemyService.OnEnemySpawned.Subscribe(EnemyService_OnEnemySpawned),
                enemyService.OnEnemyDestroyed.Subscribe(EnemySerivce_OnEnemyDestroyed)
            );
        }

        public void Start()
        {
            for (int i = 0; i < enemyService.Enemies.Count; i++)
            {
                EnemyService_OnEnemySpawned(enemyService.Enemies[i]);
            }
        }

        public void Dispose()
        {
            disposable.Dispose();
        }

        private void EnemyService_OnEnemySpawned(EnemyMB enemy)
        {
            EnemyHealthVMB model = enemyHealthViewModelPool.Get();
            model.Enemy = enemy;
        }

        private void EnemySerivce_OnEnemyDestroyed(EnemyMB enemy)
        {
            for (int i = enemyHealthViewModels.Count - 1; i > -1; i--)
            {
                EnemyHealthVMB model = enemyHealthViewModels[i];

                if (model.Enemy != enemy)
                    continue;

                enemyHealthViewModelPool.Release(model);
                break;
            }
        }
    }
}