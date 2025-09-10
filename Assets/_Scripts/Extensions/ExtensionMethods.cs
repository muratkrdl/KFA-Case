using UnityEngine;

namespace _Scripts.Extensions
{
    public static class ExtensionMethods
    {
        public static void GetReference<T>(this MonoBehaviour mono, ref T reference) where T : Object
        {
            if (reference) return;
            
            reference = mono.GetComponent<T>();
        }
    }
}