using UnityEngine;
using _Scripts.Data.ValueObjects.Player;

namespace _Scripts.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "new CD_PLAYER", menuName = "Data/CD/PLAYER")]
    public class CD_PLAYER : ScriptableObject
    {
        public PlayerMovementData MovementData;
        public PlayerDetectionData DetectionData;
        public PlayerIFrameData IFrameData;
        public PlayerHealthData HealthData;
        public PlayerKnockbackData KnockbackData;
        public PlayerProjectileData ProjectileData;
    }
}