using MVC.Common;
using MVC.Game.Logic.Interfaces.Player;
using MVC.Game.Logic.ScriptableObjects;
using MVC.Game.Model.Player;
using R3;
using R3.Triggers;
using System;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Game.Logic.Services.Player
{
    public class PlayerHealthService : IPlayerHealthService, IInitializable, IStartable, IDisposable
    {
        private readonly PlayerHealthSettings healthSettings;

        private PlayerHealthModel healthData;

        private IPlayerService playerService;

        private Subject<ChangedValue<float>> onHealthChanged;
        private Subject<ChangedValue<float>> onMaxHealthChanged;

        private IDisposable disposable;

        public PlayerHealthModel HealthData => healthData;

        public Subject<ChangedValue<float>> OnHealthChanged => onHealthChanged;
        public Subject<ChangedValue<float>> OnMaxHealthChanged => onMaxHealthChanged;

        public PlayerHealthService(PlayerHealthSettings healthSettings, IPlayerService playerService)
        {
            this.healthSettings = healthSettings;
            this.playerService = playerService;
        }

        public void Initialize()
        {
            healthData = new PlayerHealthModel(healthSettings.MaxHealth);

            onHealthChanged = new Subject<ChangedValue<float>>();
            onMaxHealthChanged = new Subject<ChangedValue<float>>();
        }

        public void Start()
        {
            onMaxHealthChanged.OnNext(new ChangedValue<float>(0.0f, healthData.Health));
            onHealthChanged.OnNext(new ChangedValue<float>(0.0f, healthData.MaxHealth));

            disposable = playerService.Player.OnTriggerEnterAsObservable()
                .Where(x => x.CompareTag("HealZone"))
                .Subscribe((Collider collider) =>
                {
                    IncreaseHealth(10.0f);
                });
        }

        public void Dispose()
        {
            disposable.Dispose();
        }

        public void ChangeMaxHealth(float maxHealth)
        {
            float previousValue = healthData.MaxHealth;
            healthData.SetMaxHealth(maxHealth);
            OnMaxHealthChanged.OnNext(new ChangedValue<float>(previousValue, healthData.MaxHealth));
        }

        public void IncreaseHealth(float value)
        {
            float previousValue = healthData.Health;
            healthData.IncreaseHealth(value);
            OnHealthChanged.OnNext(new ChangedValue<float>(previousValue, healthData.Health));
        }
    }
}
