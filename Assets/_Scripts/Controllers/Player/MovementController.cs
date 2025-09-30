using _Scripts.Data.UnityObjects;
using _Scripts.Data.ValueObjects;
using _Scripts.Extensions;
using Unity.Mathematics;
using UnityEngine;

namespace _Scripts.Controllers.Player
{
    public class MovementController : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private PlayerMovementData _data;
        
        private Vector3 _moveInput;
        
        private void Awake()
        {
            this.GetReference(ref _rigidbody);

            _data = Resources.Load<CD_PLAYER>("Data/CD_PLAYER").MovementData;
        }

        public void HandleMoveInput(float3  input)
        {
            _moveInput = input;
        }
        
        void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + Time.fixedDeltaTime * _data.MoveSpeed * _moveInput);
        }

    }
}