using _Scripts.Events;
using _Scripts.Managers.ServiceLocator;
using Unity.Mathematics;
using UnityEngine;

namespace _Scripts.Controllers.Player
{
    public class InputController : MonoBehaviour
    {
        private InputEvents _inputEvents;

        private void Awake()
        {
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
            Debug.Log("OnMoveStart" + value);
        }

        private void OnMoveStop(float2 value)
        {
            Debug.Log("OnMoveStop" + value);
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