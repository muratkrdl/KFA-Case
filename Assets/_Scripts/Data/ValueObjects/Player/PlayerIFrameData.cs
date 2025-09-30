using System;
using UnityEngine;

namespace _Scripts.Data.ValueObjects.Player
{
    [Serializable]
    public struct PlayerIFrameData
    {
        public float IFrameDuration;
        public AnimationCurve Ease;
    }
}