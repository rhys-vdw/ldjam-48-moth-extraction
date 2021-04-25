using UnityEngine;
#if UNITY_EDITOR
  using UnityEditor;
#endif

namespace Moth {
  public static class PolygonUtility {
    public static Mesh PolygonCollider2DToMesh(PolygonCollider2D polygon) {
      var pointCount = polygon.GetTotalPointCount();
      var mesh = new Mesh();
#if UNITY_EDITOR
      Undo.RegisterCreatedObjectUndo(mesh, "Generate mesh");
#endif
      var points = polygon.points;
      var vertices = new Vector3[pointCount];
      var uv = new Vector2[pointCount];
      for (var i = 0; i < pointCount; i++) {
        var actual = points[i];
        vertices[i] = new Vector3(actual.x, actual.y, 0);
        uv[i] = actual;
      }

      var triangles = new Triangulator(points).Triangulate();
      mesh.vertices = vertices;
      mesh.triangles = triangles;
      mesh.uv = uv;
      return mesh;
    }
  }
}