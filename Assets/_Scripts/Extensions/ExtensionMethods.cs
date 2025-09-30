using _Scripts.Utilities;
using Unity.Mathematics;
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
        
        public static void GetReferenceChildren<T>(this MonoBehaviour mono, ref T reference) where T : Object
        {
            if (reference) return;
            
            reference = mono.GetComponentInChildren<T>();
        }

        public static T GetJustReference<T>(this MonoBehaviour mono) where T : Object
        {
            return mono.GetComponent<T>();
        }

        public static float SqrMagnitude(this float3 f)
        {
            return new Vector3(f.x, f.y, f.z).sqrMagnitude;
        }

        public static void LookCamera(this Transform t)
        {
            t.forward = ConstUtilities.MainCamera.transform.forward;
        }

        public static void SetAlpha(this SpriteRenderer spriteRenderer, float alpha)
        {
            Color color = spriteRenderer.color;
            spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
        }
        
    }
}