using MVC.Models;

namespace MVC.Data.Player
{
    public record PlayerData
    {
        private PlayerModel model;

        public PlayerModel Model { get => model; set => model = value; }
    }
}
