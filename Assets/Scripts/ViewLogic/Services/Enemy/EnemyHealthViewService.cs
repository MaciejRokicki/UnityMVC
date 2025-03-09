using MVC.Data.Enemy;
using MVC.Logic.Interfaces.Enemy;
using MVC.ViewLogic.Interfaces.Enemy;
using MVC.ViewModels;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using VContainer.Unity;

namespace MVC.ViewLogic.Services.Enemy
{
    public class EnemyHealthViewService : IEnemyHealthViewService, IInitializable, IStartable, IDisposable
    {
        private readonly Transform enemyHealthContainer;
        private readonly EnemyHealthViewModel enemyHealthViewModelPrefab;

        private readonly IEnemyService enemyService;

        private List<EnemyHealthViewModel> enemyHealthViewModels;
        private IObjectPool<EnemyHealthViewModel> enemyHealthViewModelPool;
        private IDisposable disposable;

        public EnemyHealthViewService(
            Transform enemyHealthContainer,
            EnemyHealthViewModel enemyHealthViewModelPrefab,
            IEnemyService enemyService)
        {
            this.enemyHealthContainer = enemyHealthContainer;
            this.enemyHealthViewModelPrefab = enemyHealthViewModelPrefab;
            this.enemyService = enemyService;
        }

        public void Initialize()
        {
            enemyHealthViewModels = new List<EnemyHealthViewModel>();

            enemyHealthViewModelPool = new ObjectPool<EnemyHealthViewModel>(
                () =>
                {
                    EnemyHealthViewModel enemyHealthViewModel = GameObject.Instantiate(
                        enemyHealthViewModelPrefab, enemyHealthContainer);
                    return enemyHealthViewModel;
                }, (EnemyHealthViewModel enemyHealthViewModel) =>
                {
                    enemyHealthViewModel.gameObject.SetActive(true);
                    enemyHealthViewModels.Add(enemyHealthViewModel);
                }, (EnemyHealthViewModel enemyHealthViewModel) =>
                {
                    enemyHealthViewModels.Remove(enemyHealthViewModel);
                    enemyHealthViewModel.EnemyData = null;
                    enemyHealthViewModel.gameObject.SetActive(false);
                }, (EnemyHealthViewModel enemyHealthViewModel) =>
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

        private void EnemyService_OnEnemySpawned(EnemyData enemyData)
        {
            EnemyHealthViewModel model = enemyHealthViewModelPool.Get();
            model.EnemyData = enemyData;
        }

        private void EnemySerivce_OnEnemyDestroyed(EnemyData enemyData)
        {
            for (int i = enemyHealthViewModels.Count - 1; i > -1; i--)
            {
                EnemyHealthViewModel model = enemyHealthViewModels[i];

                if (model.EnemyData != enemyData)
                    continue;

                enemyHealthViewModelPool.Release(model);
                break;
            }
        }
    }
}