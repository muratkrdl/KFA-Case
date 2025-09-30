using UnityEngine;

namespace _Scripts.Utilities
{
    public static class ConstUtilities
    {

#region Strings

        public const string COLOUR_REF = "_Colour";

        public const string SINGLE = "Single";
        public const string LOOP = "Loop";

        public const string LAST_DIR_X = "LastDirX";
        public const string LAST_DIR_Y = "LastDirY";
        public const string SPEED = "Speed";
        
#endregion

#region GameObjects

        public static readonly Camera MainCamera = Camera.main;

#endregion

#region Vectors

        public static readonly Vector2 Zero2 = Vector2.zero;
        
        public static readonly Vector3 Zero3 = Vector3.zero;
        public static readonly Vector3 One3 = Vector3.one;
        public static readonly Vector3 OutOfWorld3 = new Vector3(-999,-999,-999);

#endregion

#region AnimHash

        public static readonly int Anim = Animator.StringToHash("Anim");

#endregion

#region Tags

        public const string ENEMY_TAG = "Enemy";

#endregion

#region Layers

        public static readonly int PlayerCollider = LayerMask.NameToLayer("PlayerCollider");

#endregion

    }
}