using System.Collections.Generic;
using _Scripts.Keys;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Managers
{
    public class PathKeeper : MonoBehaviour
    {
        [SerializeField] private List<PathList> paths = new();
        
        public float3[] GetPath()
        {
            var path = paths[Random.Range(0, paths.Count)];
            
            float3[] nativePath = new float3[path.path.Length];
            for (int i = 0; i < path.path.Length; i++)
                nativePath[i] = path.path[i].position;
            return nativePath;
        }
        
    }
}