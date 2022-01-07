using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace SerV112.UtilityAI.Game
{
    //[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
    public class InputReader : DescriptionBaseSO, GameInput.IGameplayActions
    {

        public event UnityAction RunningStartedEvent = delegate { };
        public event UnityAction RunningCanceledEvent = delegate { };
        public event UnityAction AimingStartedEvent = delegate { };
        public event UnityAction AimingCanceledEvent = delegate { };
        public event UnityAction<Vector2> MoveEvent = delegate { };
        public event UnityAction ShootingStartedEvent = delegate { };
        public event UnityAction ShootingCanceledEvent = delegate { };
        public event UnityAction ThrowEvent = delegate { };
        public event UnityAction JumpEvent = delegate { };
        private GameInput _gameInput;

        private void OnEnable()
        {
          
            if (_gameInput == null)
            {
                _gameInput = new GameInput();
                _gameInput.Gameplay.SetCallbacks(this);

            }


           
        }

        private void OnDisable()
        {

            DisableAllInput();
        }

        public void DisableAllInput()
        {
            _gameInput.Gameplay.Disable();
        }
        public void EnableGameplayInput()
        {
            _gameInput.Gameplay.Enable();

        }
        public void DisableGameplayInput()
        {
            _gameInput.Gameplay.Disable();

        }
        public void OnAim(InputAction.CallbackContext context)
        {

            if (context.phase == InputActionPhase.Performed)
                AimingStartedEvent.Invoke();

            if (context.phase == InputActionPhase.Canceled)
                AimingCanceledEvent.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {

            MoveEvent.Invoke(context.ReadValue<Vector2>());
        }

        public void OnRun(InputAction.CallbackContext context)
        {

            if (context.phase == InputActionPhase.Performed)
                RunningStartedEvent.Invoke();

            if (context.phase == InputActionPhase.Canceled)
                RunningCanceledEvent.Invoke();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {

            if (context.phase == InputActionPhase.Performed)
                ShootingStartedEvent.Invoke();

            if (context.phase == InputActionPhase.Canceled)
                ShootingCanceledEvent.Invoke();
        }

        public void OnThrow(InputAction.CallbackContext context)
        {

            if (context.phase == InputActionPhase.Performed)
                ThrowEvent.Invoke();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                JumpEvent.Invoke();
        }
    }

}
