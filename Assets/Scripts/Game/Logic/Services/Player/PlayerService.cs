using MVC.Game.Logic.Interfaces.Player;
using MVC.Game.Logic.MonoBehaviours;
using VContainer;
using VContainer.Unity;

namespace MVC.Game.Logic.Services.Player
{
    public class PlayerService : IPlayerService, IStartable
    {
        private readonly PlayerMB playerPrefab;
        private readonly IObjectResolver objectResolver;

        private PlayerMB player;
        public PlayerMB Player => player;

        public PlayerService(PlayerMB playerPrefab, IObjectResolver objectResolver)
        {
            this.playerPrefab = playerPrefab;
            this.objectResolver = objectResolver;
        }

        public void Start()
        {
            player = objectResolver.Instantiate(playerPrefab);
        }
    }
}
