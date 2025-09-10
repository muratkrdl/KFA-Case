using _Scripts.Events;
using _Scripts.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Managers
{
    public class InputManager : MonoBehaviour
    { 
        private PlayerInputActions _playerInputActions;

        private InputEvents _inputEvents;
        
        protected virtual void Awake()
        {
            _inputEvents = ServiceLocator.ServiceLocator.Get<InputEvents>();
            
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
            _playerInputActions.UI.Enable();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _playerInputActions.Player.Move.started += OnMoveStart;
            _playerInputActions.Player.Move.canceled += OnMoveStop;
        }

        private void OnMoveStart(InputAction.CallbackContext obj)
        {
            _inputEvents.OnMoveStart?.Invoke(obj.ReadValue<Vector2>());
        }

        private void OnMoveStop(InputAction.CallbackContext obj)
        {
            _inputEvents.OnMoveStop?.Invoke(ConstUtilities.Zero2);
        }
        
        private void UnSubscribeEvents()
        {
            _playerInputActions.Player.Move.started -= OnMoveStart;
            _playerInputActions.Player.Move.canceled -= OnMoveStop;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
    }
}