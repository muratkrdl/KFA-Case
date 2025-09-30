using _Scripts.Events;
using _Scripts.Extensions;
using _Scripts.Managers.ServiceLocator;
using Unity.Mathematics;
using UnityEngine;

namespace _Scripts.Controllers.Player
{
    public class InputController : MonoBehaviour
    {
        private MovementController _movementController;
        
        private InputEvents _inputEvents;

        private void Awake()
        {
            this.GetReference(ref _movementController);
            _inputEvents = ServiceLocator.Get<InputEvents>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _inputEvents.OnMoveStart += OnMoveStart;
            _inputEvents.OnMoveStop += OnMoveStop;
        }

        private void OnMoveStart(float2 value)
        {
            Vector3 realValue = new Vector3(value.x, 0, value.y);
            _movementController.HandleMoveInput(realValue);
        }

        private void OnMoveStop(float2 value)
        {
            Vector3 realValue = new Vector3(value.x, 0, value.y);
            _movementController.HandleMoveInput(realValue);
        }

        private void UnSubscribeEvents()
        {
            _inputEvents.OnMoveStart -= OnMoveStart;
            _inputEvents.OnMoveStop -= OnMoveStop;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}