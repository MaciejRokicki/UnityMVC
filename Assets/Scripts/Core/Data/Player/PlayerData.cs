using MVC.Core.Models;

namespace MVC.Core.Data.Player
{
    public record PlayerData
    {
        private PlayerModel model;

        public PlayerModel Model { get => model; set => model = value; }
    }
}
