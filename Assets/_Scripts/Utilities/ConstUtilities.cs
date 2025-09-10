using UnityEngine;

namespace _Scripts.Utilities
{
    public static class ConstUtilities
    {
        
#region Strings

        public const string SINGLE = "Single";
        public const string LOOP = "Loop";

#endregion

#region GameObjects

        public static readonly Camera MainCamera = Camera.main;

#endregion

#region Vectors

        public static readonly Vector2 Zero2 = Vector2.zero;
        
        public static readonly Vector3 Zero3 = Vector3.zero;

#endregion

    }
}