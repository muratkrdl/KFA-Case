using _Scripts.Data.ValueObjects.Player;
using _Scripts.Extensions;
using Unity.Mathematics;
using UnityEngine;

namespace _Scripts.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        private PlayerMovementData _data;

        private Rigidbody _rigidbody;
        
        private Vector3 _moveInput;
        
        private void Awake()
        {
            this.GetReference(ref _rigidbody);
        }

        public void HandleMoveInput(float3  input)
        {
            _moveInput = input;
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + Time.fixedDeltaTime * _data.MoveSpeed * _moveInput);
        }

        public void SetData(PlayerMovementData data)
        {
            _data = data;
        }

    }
}