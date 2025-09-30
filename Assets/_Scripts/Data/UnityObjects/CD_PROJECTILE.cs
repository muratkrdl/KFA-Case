using _Scripts.Data.ValueObjects;
using _Scripts.Data.ValueObjects.Player;
using _Scripts.Data.ValueObjects.Projectile;
using UnityEngine;

namespace _Scripts.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "new CD_PROJECTILE", menuName = "Data/CD/CD_PROJECTILE", order = 0)]
    public class CD_PROJECTILE : ScriptableObject
    {
        public ProjectileMoveData ProjectileMoveData;
        public ProjectileDamageData ProjectileDamageData;
    }
}