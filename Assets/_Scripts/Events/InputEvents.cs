using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Events
{
    public class InputEvents : MonoBehaviour
    {
        public UnityAction<float2> OnMoveStart;
        public UnityAction<float2> OnMoveStop;
    }
}