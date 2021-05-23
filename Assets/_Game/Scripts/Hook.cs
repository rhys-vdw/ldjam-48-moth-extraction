using UnityEngine;
using EasyButtons;

namespace Moth {
  public class Hook : MonoBehaviour {
    #pragma warning disable 414
    [SerializeField] Transform _hookPrefab = null;
    [SerializeField] Transform _segmentPrefab = null;
    [SerializeField] int _segmentCount = 5;
    #pragma warning restore 414

  #if UNITY_EDITOR
    [Button]
    void Generate() {
      while (transform.childCount > 0) {
        DestroyImmediate(transform.GetChild(0).gameObject);
      }
      var nextPosition = transform.position;
      HingeJoint2D previousJoint = null;
      for (var i = 0; i < _segmentCount; i++) {
        var segment = Instantiate(
          original: _segmentPrefab,
          position: nextPosition,
          rotation: Quaternion.identity,
          parent: transform
        );
        if (previousJoint != null) {
          var rb = segment.GetComponent<Rigidbody2D>();
          Debug.Assert(rb != null, nameof(Rigidbody2D));
          previousJoint.connectedBody = rb;
        }
        previousJoint = segment.GetComponentInChildren<HingeJoint2D>();
        Debug.Assert(previousJoint != null, "No joint found");
        nextPosition += Vector3.right;
      }
      var hook = Instantiate(
        original: _hookPrefab,
        position: nextPosition,
        rotation: Quaternion.identity,
        parent: transform
      );
      if (previousJoint != null) {
        var rb = hook.GetComponent<Rigidbody2D>();
        Debug.Assert(rb != null, nameof(Rigidbody2D));
        previousJoint.connectedBody = hook.GetComponentInChildren<Rigidbody2D>();
      }
    }
#endif
  }
}