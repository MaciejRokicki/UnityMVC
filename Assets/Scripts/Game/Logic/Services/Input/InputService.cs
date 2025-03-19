using MVC.Game.Logic.Interfaces;
using MVC.Game.Logic.Interfaces.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace MVC.Game.Logic.Input
{
    public class InputService : InputSystemActions.IPlayerActions, IInitializable, ITickable
    {
        private readonly IPlayerMovementService playerMovementService;
        private readonly IPlayerCombatService playerCombatService;
        private readonly IPlayerAbilityService playerAbilityService;

        private InputSystemActions inputSystemActions;

        private Vector3 movementDirection;

        public InputService(
            IPlayerMovementService playerMovementService,
            IPlayerCombatService playerCombatService,
            IPlayerAbilityService playerAbilityService)
        {
            this.playerMovementService = playerMovementService;
            this.playerCombatService = playerCombatService;
            this.playerAbilityService = playerAbilityService;
        }

        public void Initialize()
        {
            inputSystemActions = new InputSystemActions();
            inputSystemActions.Player.SetCallbacks(this);
            inputSystemActions.Player.Enable();
        }

        public void Tick()
        {
            playerMovementService.Move(movementDirection);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                movementDirection = context.ReadValue<Vector3>();
            }
            else
            {
                movementDirection = Vector3.zero;
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                playerCombatService.Attack();
            }
        }

        public void OnAbility1(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                playerAbilityService.Ability1();
            }
        }

        public void OnAbility2(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                playerAbilityService.Ability2();
            }
        }
    }
}