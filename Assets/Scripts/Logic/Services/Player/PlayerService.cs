using MVC.Data.Player;
using MVC.Logic.Interfaces.Player;
using MVC.Models;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Logic.Services.Player
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
