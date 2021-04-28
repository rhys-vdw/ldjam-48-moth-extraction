using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalBounce : MonoBehaviour {
  [SerializeField] float _amplitude = 0.1f;
  [SerializeField] float _period = 1f;

  const float TwoPi = 2 * Mathf.PI;

  void Update() {
    if (_period != 0) {
      transform.localPosition = new Vector3(
        0,
        _amplitude * Mathf.Sin(TwoPi / _period * Time.time),
        0
      );
    }
  }
}