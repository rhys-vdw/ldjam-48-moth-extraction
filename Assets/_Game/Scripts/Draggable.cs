using System;
using UnityEngine;

namespace Moth {
  public class Draggable : MonoBehaviour {
    [SerializeField] SpriteRenderer[] _spriteRenderers = null;
    Rigidbody2D _rigidbody2D = null;
    Camera _camera;
    Vector2 _prevPosition;
    bool _isDragging = false;
    static readonly Plane _plane = new Plane(Vector3.back, Vector3.zero);
    Color _defaultColor;
    [SerializeField] Color _dragColor = Color.white;

    public static bool IsAnyDragging { get; private set; }

    void Awake() {
      _camera = Camera.main;
      _rigidbody2D = GetComponent<Rigidbody2D>();
      _defaultColor = _spriteRenderers[0].color;
    }

    Vector2 GetPosition() {
      var ray = _camera.ScreenPointToRay(Input.mousePosition);
      if (_plane.Raycast(ray, out var enter)) {
        return ray.GetPoint(enter);
      }

      Debug.LogError("Could not find world position");
      return Vector2.zero;
    }

    void OnMouseDown() {
      _prevPosition = GetPosition();
      _isDragging = true;
      IsAnyDragging = true;
      // _rigidbody2D.isKinematic = true;
      SetColor(_dragColor);
    }

    void SetColor(Color color) {
      foreach (var r in _spriteRenderers) r.color = color;
    }

    void Update() {
      if (_isDragging && Input.GetMouseButtonUp(0)) {
        _isDragging = false;
        IsAnyDragging = false;
        // _rigidbody2D.isKinematic = false;
        SetColor(_defaultColor);
      }
    }

    void FixedUpdate() {
      if (_isDragging) {
        var position = GetPosition();
        // var delta = position - _prevPosition;
        // Debug.DrawLine(_prevPosition, position, Color.red);
        // _prevPosition = position;
        _rigidbody2D.MovePosition(position);
      }
    }
  }
}