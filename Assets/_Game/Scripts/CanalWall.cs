using UnityEngine;
using EasyButtons;

namespace Moth {
  public class CanalWall : MonoBehaviour {
    #pragma warning disable 414
    [SerializeField] PolygonCollider2D _polygon = null;
    [SerializeField] MeshFilter _meshFilter = null;
    #pragma warning restore 414

  #if UNITY_EDITOR
    [Button]
    void RegenerateMesh() {
      if (_meshFilter.sharedMesh != null) {
        DestroyImmediate(_meshFilter.sharedMesh);
      }
      _meshFilter.sharedMesh = PolygonUtility.PolygonCollider2DToMesh(_polygon);
      UnityEditor.Undo.RecordObject(_meshFilter, "Assigned mesh");
    }
  #endif
  }
}