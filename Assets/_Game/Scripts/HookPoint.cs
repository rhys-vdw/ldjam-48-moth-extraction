using System;
using UnityEngine;

namespace Moth
{
  public class HookPoint : MonoBehaviour
  {
    [SerializeField] Rigidbody2D _rigidbody2D = null;
    [SerializeField] float _maxBreakForce = Mathf.Infinity;

    FixedJoint2D _joint = null;

    void OnTriggerEnter2D(Collider2D other) {
      // Debug.Log(
      //   $"Triggered by object (layer = {LayerMask.LayerToName(other.gameObject.layer)})",
      //   other
      // );
      var layer = LayerMask.NameToLayer("Moth");
      if (_joint == null && other.gameObject.layer == layer) {
        _joint = _rigidbody2D.gameObject.AddComponent<FixedJoint2D>();
        var t = _rigidbody2D.transform;
        _joint.anchor = t.InverseTransformPoint(transform.position);
        _joint.connectedBody = other.attachedRigidbody;
        _joint.connectedAnchor = t.InverseTransformPoint(
          other.ClosestPoint(transform.position)
        );
        _joint.breakForce = _maxBreakForce;
        // Debug.Log("Connected to moth", _joint);
      }
    }
  }
}