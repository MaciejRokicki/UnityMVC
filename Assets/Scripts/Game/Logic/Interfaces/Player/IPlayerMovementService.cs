using MVC.Common;
using MVC.Game.Model.Player;
using R3;
using UnityEngine;

namespace MVC.Game.Logic.Interfaces
{
    public interface IPlayerMovementService
    {
        public PlayerMovementModel MovementData { get; }

        public Subject<ChangedValue<float>> OnMovementSpeedChanged { get; }

        public void ChangeMovementSpeed(float speed);
        public void Move(Vector3 direction);
    }
}