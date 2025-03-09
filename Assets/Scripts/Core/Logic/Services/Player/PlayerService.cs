using MVC.Core.Data.Player;
using MVC.Core.Logic.Interfaces.Player;
using MVC.Core.Models;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Core.Logic.Services.Player
{
    public class PlayerService : IPlayerService, IStartable
    {
        private readonly PlayerModel playerModel;

        private PlayerData playerData = new PlayerData();

        public PlayerData PlayerData => playerData;

        public PlayerService(PlayerModel playerModel)
        {
            this.playerModel = playerModel;
        }

        public void Start()
        {
            PlayerModel player = GameObject.Instantiate(playerModel);
            playerData.Model = player;
        }
    }
}
