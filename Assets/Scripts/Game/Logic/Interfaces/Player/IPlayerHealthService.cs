using MVC.Common;
using MVC.Game.Model.Player;
using R3;

namespace MVC.Game.Logic.Interfaces.Player
{
    public interface IPlayerHealthService
    {
        public PlayerHealthModel HealthData { get; }

        public Subject<ChangedValue<float>> OnHealthChanged { get; }
        public Subject<ChangedValue<float>> OnMaxHealthChanged { get; }

        public void ChangeMaxHealth(float maxHealth);
        public void IncreaseHealth(float value);
    }
}
