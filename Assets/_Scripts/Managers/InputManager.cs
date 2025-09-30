using _Scripts.Events;
using _Scripts.Utilities;
using Unity.Mathematics;
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
            _playerInputActions.Player.Move.performed += OnMoveStart;
            _playerInputActions.Player.Move.canceled += OnMoveStop;
        }

        private void OnMoveStart(InputAction.CallbackContext obj)
        {
            var vec2 = obj.ReadValue<Vector2>();
            float3 realValue = new float3(vec2.x, 0, vec2.y);
            _inputEvents.OnMoveStart?.Invoke(realValue);
        }

        private void OnMoveStop(InputAction.CallbackContext obj)
        {
            _inputEvents.OnMoveStop?.Invoke(ConstUtilities.Zero3);
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