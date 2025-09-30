using _Scripts.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts
{
    [RequireComponent(typeof(Tilemap))]
    public class TilemapTo3DCollider : MonoBehaviour
    {
        [SerializeField] private bool visualizeColliders;
        
        [SerializeField] private float tileThickness = 1f;

        private void Start()
        {
            Tilemap tilemap = GetComponent<Tilemap>();
            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {
                if (!tilemap.HasTile(pos)) continue;
             
                TileBase tile = tilemap.GetTile(pos);
                
                if (tile is not Tile tileAsset || !tileAsset.sprite) continue;
                
                Vector3 worldPos = tilemap.CellToWorld(pos);

                GameObject colObj = visualizeColliders 
                    ? GameObject.CreatePrimitive(PrimitiveType.Cube) 
                    : new GameObject("Collider");

                colObj.transform.parent = transform;
                colObj.transform.position = worldPos + new Vector3(0.5f, 0f, 0.5f);

                Vector2 spriteSize = tileAsset.sprite.bounds.size;

                colObj.layer = ConstUtilities.PlayerCollider;
                colObj.transform.localScale = new Vector3(spriteSize.x, 1f, spriteSize.y) * tileThickness;

                if (!colObj.GetComponent<BoxCollider>())
                    colObj.AddComponent<BoxCollider>();
            }
        }
    }
}