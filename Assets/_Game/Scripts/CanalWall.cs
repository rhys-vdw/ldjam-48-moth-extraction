using UnityEngine;
using EasyButtons;

namespace Moth
{
  public class CanalWall : MonoBehaviour
  {
    [SerializeField] PolygonCollider2D _polygon = null;
    [SerializeField] MeshFilter _meshFilter = null;

    [Button]
    void RegenerateMesh() {
      if (_meshFilter.sharedMesh != null) Destroy(_meshFilter.sharedMesh);
      _meshFilter.sharedMesh = PolygonUtility.PolygonCollider2DToMesh(_polygon);
  #if UNITY_EDITOR
      UnityEditor.Undo.RecordObject(_meshFilter, "Assigned mesh");
  #endif
    }
  }
}