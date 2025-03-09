using MVC.Common;
using MVC.Data.Player;
using R3;

namespace MVC.Logic.Interfaces.Player
{
    public interface IPlayerCombatService
    {
        public PlayerCombatData CombatData { get; }

        public Subject<ChangedValue<float>> OnDamageChanged { get; }

        public void ChangeDamage(float damage);
        public void Attack();
    }
}