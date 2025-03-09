using MVC.Common;
using MVC.Core.Data.Player;
using R3;

namespace MVC.Core.Logic.Interfaces.Player
{
    public interface IPlayerCombatService
    {
        public PlayerCombatData CombatData { get; }

        public Subject<ChangedValue<float>> OnDamageChanged { get; }

        public void ChangeDamage(float damage);
        public void Attack();
    }
}