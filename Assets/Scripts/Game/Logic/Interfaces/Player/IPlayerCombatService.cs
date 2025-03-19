using MVC.Common;
using MVC.Game.Model.Player;
using R3;

namespace MVC.Game.Logic.Interfaces.Player
{
    public interface IPlayerCombatService
    {
        public PlayerCombatModel CombatData { get; }

        public Subject<ChangedValue<float>> OnDamageChanged { get; }

        public void ChangeDamage(float damage);
        public void Attack();
    }
}