using _Scripts.Utilities;
using UnityEngine;

namespace _Scripts
{
    public class SpriteSetter : MonoBehaviour
    {
        
        private void Start()
        {
            Transform lookTarget = ConstUtilities.MainCamera.transform;
            
            SetChildRotation(transform, lookTarget);
        }

        private void SetChildRotation(Transform parent, Transform lookTarget)
        {
            foreach (Transform child in parent)
            {
                if (child.TryGetComponent<SpriteRenderer>(out _))
                {
                    child.forward = lookTarget.forward;
                }
                else
                {
                    SetChildRotation(child, lookTarget);
                }
            }
        }
        
    }
}
