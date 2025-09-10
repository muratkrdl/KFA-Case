using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.Data.UnityObjects.SO
{
    [CreateAssetMenu(fileName = "New SFXData", menuName = "SO/SFXData")]
    public class SFXData : ScriptableObject
    {
        public string Name;
        
        public AudioClip[] Clips;

        public SFXType type;
        
        public bool Loop;
        
        [Range(0,1)] public float Volume = .5f;
        [Range(-3,3)] public float MinPitch = 1f;
        [Range(-3,3)] public float MaxPitch = 1f;
        [Range(0,1)] public float Is3D;
        
        public float MaxDistance = 500f;

        public AudioClip GetClip()
        {
            return Clips[Random.Range(0, Clips.Length)];
        }

        public float GetPitch()
        {
            return Random.Range(MinPitch, MaxPitch);
        }
        
    }
}