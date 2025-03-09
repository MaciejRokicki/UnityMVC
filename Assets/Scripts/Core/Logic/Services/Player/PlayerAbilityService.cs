using MVC.Core.Logic.Interfaces.Player;
using MVC.Core.Logic.Services.Player.CombatAbility;
using VContainer.Unity;

namespace MVC.Core.Logic.Services.Player
{
    public class PlayerAbilityService : IPlayerAbilityService, IInitializable
    {
        private readonly PlayerCombatAbility1Factory playerCombatAbility1Factory;
        private readonly PlayerCombatAbility2Factory playerCombatAbility2Factory;

        private PlayerCombatAbility1 playerCombatAbility1;
        private PlayerCombatAbility2 playerCombatAbility2;

        public PlayerAbilityService(
            PlayerCombatAbility1Factory playerCombatAbility1Factory, 
            PlayerCombatAbility2Factory playerCombatAbility2Factory)
        {
            this.playerCombatAbility1Factory = playerCombatAbility1Factory;
            this.playerCombatAbility2Factory = playerCombatAbility2Factory;
        }

        public void Initialize()
        {
            playerCombatAbility1 = playerCombatAbility1Factory.Create();
            playerCombatAbility2 = playerCombatAbility2Factory.Create();
        }

        public void Ability1()
        {
            playerCombatAbility1.Handle();
        }

        public void Ability2()
        {
            playerCombatAbility2.Handle();
        }
    }
}