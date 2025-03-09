using MVC.Common;
using MVC.Core.Data.Player;
using R3;
using UnityEngine;

namespace MVC.Core.Logic.Interfaces
{
    public interface IPlayerMovementService
    {
        public PlayerMovementData MovementData { get; }

        public Subject<ChangedValue<float>> OnMovementSpeedChanged { get; }

        public void ChangeMovementSpeed(float speed);
        public void Move(Vector3 direction);
    }
}