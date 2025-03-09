using MVC.Common;
using MVC.Data.Player;
using MVC.Logic.Interfaces;
using MVC.Logic.Interfaces.Player;
using MVC.Models.Settings;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Logic.Services
{
    public class PlayerMovementService : IPlayerMovementService, IInitializable
    {
        private readonly IPlayerService playerService;

        private readonly PlayerMovementSettings movementSettings;

        private PlayerMovementData movementData;
        private Subject<ChangedValue<float>> onMovementSpeedChanged;

        public PlayerMovementData MovementData => movementData;

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
            movementData = new PlayerMovementData();
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
            playerService.PlayerData.Model.transform.position += direction * movementData.Speed * Time.deltaTime;
        }
    }
}