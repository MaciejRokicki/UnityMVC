using Cysharp.Text;
using MVC.Common;
using MVC.Logic.Interfaces.Player;
using MVC.ViewLogic.Interfaces.Player;
using MVC.ViewModels;
using R3;
using System;
using VContainer.Unity;

namespace MVC.ViewLogic.Services.Player
{
    public class PlayerHealthViewService : IPlayerHealthViewService, IInitializable, IDisposable
    {
        private readonly PlayerHealthViewModel playerHealthViewModel;

        private readonly IPlayerHealthService playerHealthService;

        private IDisposable disposable;

        public PlayerHealthViewService(IPlayerHealthService playerHealthService, PlayerHealthViewModel playerHealthViewModel)
        {
            this.playerHealthService = playerHealthService;
            this.playerHealthViewModel = playerHealthViewModel;
        }

        public void Initialize()
        {
            playerHealthViewModel.Slider.maxValue = playerHealthService.HealthData.MaxHealth;
            playerHealthViewModel.Slider.value = playerHealthService.HealthData.Health;
            playerHealthViewModel.Text.SetTextFormat("{0}/{1}", playerHealthService.HealthData.Health, playerHealthService.HealthData.MaxHealth);

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
            playerHealthViewModel.Slider.value = changedValue.NewValue;
            playerHealthViewModel.Text.SetTextFormat("{0}/{1}", changedValue.NewValue, playerHealthService.HealthData.MaxHealth);
        }

        private void PlayerHealthService_OnMaxHealthChanged(ChangedValue<float> changedValue)
        {
            playerHealthViewModel.Slider.maxValue = changedValue.NewValue;
            playerHealthViewModel.Text.SetTextFormat("{0}/{1}", playerHealthService.HealthData.Health, changedValue.NewValue);
        }
    }
}
