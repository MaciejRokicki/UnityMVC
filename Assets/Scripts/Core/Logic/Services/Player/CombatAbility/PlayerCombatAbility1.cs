using MVC.Core.Logic.Interfaces.Player;

namespace MVC.Core.Logic.Services.Player.CombatAbility
{
    public class PlayerCombatAbility1Factory
    {
        private readonly IPlayerHealthService playerHealthService;

        public PlayerCombatAbility1Factory(IPlayerHealthService playerHealthService)
        {
            this.playerHealthService = playerHealthService;
        }

        public PlayerCombatAbility1 Create() => new PlayerCombatAbility1(playerHealthService);
    }

    public class PlayerCombatAbility1 : BasePlayerCombatAbility
    {
        private readonly IPlayerHealthService playerHealthService;

        public PlayerCombatAbility1(IPlayerHealthService playerHealthService)
        {
            this.playerHealthService = playerHealthService;
        }

        public override void Handle()
        {
            playerHealthService.IncreaseHealth(5.0f);
        }
    }
}