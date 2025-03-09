using MVC.Common;
using MVC.Data.Player;
using R3;
using UnityEngine;

namespace MVC.Logic.Interfaces
{
    public interface IPlayerMovementService
    {
        public PlayerMovementData MovementData { get; }

        public Subject<ChangedValue<float>> OnMovementSpeedChanged { get; }

        public void ChangeMovementSpeed(float speed);
        public void Move(Vector3 direction);
    }
}