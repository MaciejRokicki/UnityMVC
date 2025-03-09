using MVC.Core.Logic.Interfaces.Player;

namespace MVC.Core.Logic.Services.Player.CombatAbility
{
    public class PlayerCombatAbility2Factory
    {
        private readonly IPlayerHealthService playerHealthService;
        private readonly IPlayerCombatService playerCombatService;

        public PlayerCombatAbility2Factory(IPlayerHealthService playerHealthService, IPlayerCombatService playerCombatService)
        {
            this.playerHealthService = playerHealthService;
            this.playerCombatService = playerCombatService;
        }

        public PlayerCombatAbility2 Create() => new PlayerCombatAbility2(playerHealthService, playerCombatService);
    }

    public class PlayerCombatAbility2 : BasePlayerCombatAbility
    {
        private readonly IPlayerHealthService playerHealthService;
        private readonly IPlayerCombatService playerCombatService;

        public PlayerCombatAbility2(IPlayerHealthService playerHealthService, IPlayerCombatService playerCombatService)
        {
            this.playerHealthService = playerHealthService;
            this.playerCombatService = playerCombatService;
        }

        public override void Handle()
        {
            playerHealthService.IncreaseHealth(-20.0f);
            playerCombatService.ChangeDamage(playerCombatService.CombatData.Damage + 1.0f);
        }
    }
}