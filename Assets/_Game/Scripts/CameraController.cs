using System;
using UnityEngine;

namespace Moth {
  public class CameraController : MonoBehaviour {
    [SerializeField] float _maxX = 0;
    [SerializeField] float _minX = 0;
    [SerializeField] Transform _target = null;
    [SerializeField] float _scrollRegion = 0.1f;
    [SerializeField] float _inSmoothTime = 1f;
    [SerializeField] float _outSmoothTime = 1f;
    [SerializeField] float _velocityDecay = 1f;

    Vector3 _currentVelocity;
    WinTrigger _winTrigger;
    Brain _brain;

    void Awake() {
      _winTrigger = FindObjectOfType<WinTrigger>();
      _brain = FindObjectOfType<Brain>();
    }

    void LateUpdate() {
      void MoveTo(float x, float smoothTime) {
        var target = transform.position;
        target.x = x;
        transform.position = Vector3.SmoothDamp(
          transform.position,
          target,
          ref _currentVelocity,
          smoothTime
        );
      }

      if (_brain.IsDead) {
        MoveTo(_minX, _inSmoothTime);
        return;
      }

      if (_winTrigger.DidWin) {
        MoveTo(_maxX, _outSmoothTime);
        return;
      }

      if (Input.mousePosition.x > Screen.width * (1 - _scrollRegion)) {
        MoveTo(_maxX, _outSmoothTime);
        return;
      }

      if (Draggable.IsAnyDragging) {
        var targetScreenX = Camera.main.WorldToScreenPoint(_target.position).x;
        if (targetScreenX < Screen.width * _scrollRegion) {
          MoveTo(Mathf.Max(_target.position.x, _minX), _inSmoothTime);
          return;
        }
      }

      _currentVelocity = Vector3.MoveTowards(
        _currentVelocity,
        Vector3.zero,
        _velocityDecay * Time.deltaTime
      );
    }

    void OnDrawGizmosSelected() {
      Debug.DrawLine(new Vector3(_minX, 0, 0), new Vector3(_maxX, 0, 0), Color.cyan);
    }
  }
}