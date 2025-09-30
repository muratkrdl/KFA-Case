using _Scripts.Extensions;
using UnityEngine;

namespace _Scripts
{
    public class SpriteSetter : MonoBehaviour
    {
        
        private void Start()
        {
            SetChildBillboard(transform);
        }

        private void SetChildBillboard(Transform parent)
        {
            foreach (Transform child in parent)
            {
                if (child.TryGetComponent<SpriteRenderer>(out _))
                {
                    child.LookCamera();
                }
                else
                {
                    SetChildBillboard(child);
                }
            }
        }
        
    }
}
