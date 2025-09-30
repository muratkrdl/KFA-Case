using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.Data.UnityObjects.SO
{
    [CreateAssetMenu(menuName = "ECSO/FramesAsset")]
    public class EnemyAnimFrames : ScriptableObject
    {
        public Sprite[] WalkFrames;
    }
}