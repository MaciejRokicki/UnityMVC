using MVC.Common;
using MVC.Data.Player;
using R3;

namespace MVC.Logic.Interfaces.Player
{
    public interface IPlayerHealthService
    {
        public PlayerHealthData HealthData { get; }

        public Subject<ChangedValue<float>> OnHealthChanged { get; }
        public Subject<ChangedValue<float>> OnMaxHealthChanged { get; }

        public void ChangeMaxHealth(float maxHealth);
        public void IncreaseHealth(float value);
    }
}
