using _Scripts.Data.ValueObjects.Enemy;
using UnityEngine;

namespace _Scripts.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "New CD_ENEMY", menuName = "Data/CD/CD_ENEMY", order = 0)]
    public class CD_ENEMY : ScriptableObject
    {
        public EnemyMoveData MoveData;
        public EnemyTintData TintData;
        public EnemyTriggerData TriggerData;
    }
}