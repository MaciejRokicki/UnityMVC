using Cysharp.Text;
using MVC.Common;
using MVC.Game.Logic.Interfaces.Player;
using MVC.Game.View.Interfaces.Player;
using MVC.Game.View.MonoBehaviorus;
using R3;
using System;
using VContainer.Unity;

namespace MVC.Game.View.Services.Player
{
    public class PlayerHealthViewService : IPlayerHealthViewService, IInitializable, IDisposable
    {
        private readonly PlayerHealthVMB playerHealthView;

        private readonly IPlayerHealthService playerHealthService;

        private IDisposable disposable;

        public PlayerHealthViewService(IPlayerHealthService playerHealthService, PlayerHealthVMB playerHealthView)
        {
            this.playerHealthService = playerHealthService;
            this.playerHealthView = playerHealthView;
        }

        public void Initialize()
        {
            playerHealthView.Slider.maxValue = playerHealthService.HealthData.MaxHealth;
            playerHealthView.Slider.value = playerHealthService.HealthData.Health;
            playerHealthView.Text.SetTextFormat("{0}/{1}", playerHealthService.HealthData.Health, playerHealthService.HealthData.MaxHealth);

            disposable = Disposable.Combine(
                playerHealthService.OnHealthChanged.Subscribe(PlayerHealthService_OnHealthChanged),
                playerHealthService.OnMaxHealthChanged.Subscribe(PlayerHealthService_OnMaxHealthChanged)
            );
        }

        public void Dispose()
        {
            disposable.Dispose();
        }

        private void PlayerHealthService_OnHealthChanged(ChangedValue<float> changedValue)
        {
            playerHealthView.Slider.value = changedValue.NewValue;
            playerHealthView.Text.SetTextFormat("{0}/{1}", changedValue.NewValue, playerHealthService.HealthData.MaxHealth);
        }

        private void PlayerHealthService_OnMaxHealthChanged(ChangedValue<float> changedValue)
        {
            playerHealthView.Slider.maxValue = changedValue.NewValue;
            playerHealthView.Text.SetTextFormat("{0}/{1}", playerHealthService.HealthData.Health, changedValue.NewValue);
        }
    }
}
