using MVC.Common;
using MVC.Game.Logic.Interfaces;
using MVC.Game.Logic.Interfaces.Player;
using MVC.Game.Logic.ScriptableObjects;
using MVC.Game.Model.Player;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Game.Logic.Services
{
    public class PlayerMovementService : IPlayerMovementService, IInitializable
    {
        private readonly IPlayerService playerService;

        private readonly PlayerMovementSettings movementSettings;

        private PlayerMovementModel movementData;
        private Subject<ChangedValue<float>> onMovementSpeedChanged;

        public PlayerMovementModel MovementData => movementData;

        public Subject<ChangedValue<float>> OnMovementSpeedChanged => onMovementSpeedChanged;

        public PlayerMovementService(
            IPlayerService playerService,
            PlayerMovementSettings movementSettings)
        {
            this.playerService = playerService;
            this.movementSettings = movementSettings;
        }

        public void Initialize()
        {
            movementData = new PlayerMovementModel();
            movementData.Speed = movementSettings.Speed;
            onMovementSpeedChanged = new Subject<ChangedValue<float>>();
        }

        public void ChangeMovementSpeed(float speed)
        {
            float previousValue = movementData.Speed;
            movementData.Speed = speed;
            OnMovementSpeedChanged.OnNext(new ChangedValue<float>(previousValue, movementData.Speed));
        }

        public void Move(Vector3 direction)
        {
            playerService.Player.transform.position += direction * movementData.Speed * Time.deltaTime;
        }
    }
}