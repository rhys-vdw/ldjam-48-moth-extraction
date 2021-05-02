using System;
using UnityEngine;

namespace Moth {
  public class Draggable : MonoBehaviour {
    [SerializeField] SpriteRenderer[] _spriteRenderers = null;
    Rigidbody2D _rigidbody2D = null;
    Camera _camera;
    bool _isDragging = false;
    static readonly Plane _plane = new Plane(Vector3.back, Vector3.zero);
    Color _defaultColor;
    [SerializeField] float _dragSpeed = 1f;
    [SerializeField] Color _dragColor = Color.white;
    [SerializeField] float _rotateSpeed = 180f;
    [SerializeField] bool _kinematicOnDrag = true;

    public static bool IsAnyDragging { get; private set; }
    public static event Action OnFirstDrag;
    static bool _hasAnyDragged = false;

    void Awake() {
      _camera = Camera.main;
      _rigidbody2D = GetComponent<Rigidbody2D>();
      _defaultColor = _spriteRenderers[0].color;
    }

    void OnMouseDown() {
      _isDragging = true;
      IsAnyDragging = true;
      Cursor.visible = false;
      Cursor.lockState = CursorLockMode.Confined;
      if (!_hasAnyDragged) {
        OnFirstDrag();
        _hasAnyDragged = true;
      }
      _rigidbody2D.isKinematic = _kinematicOnDrag;
      SetColor(_dragColor);
    }

    void SetColor(Color color) {
      foreach (var r in _spriteRenderers) r.color = color;
    }

    void Update() {
      if (_isDragging && Input.GetMouseButtonUp(0)) {
        StopDragging();
      }
    }

    void OnDestroy() {
      StopDragging();
    }

    void StopDragging() {
      if (!_isDragging) return;
      _isDragging = false;
      IsAnyDragging = false;
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
      _rigidbody2D.isKinematic = false;
      SetColor(_defaultColor);
    }

    static readonly Quaternion PreferredRotation = Quaternion.Euler(0, 0, 180f);

    void FixedUpdate() {
      if (_isDragging) {
        _rigidbody2D.MoveRotation(
          Quaternion.RotateTowards(
            transform.rotation,
            PreferredRotation,
            _rotateSpeed * Time.deltaTime
          )
        );
        var delta = new Vector2(
          Input.GetAxis("Mouse X"),
          Input.GetAxis("Mouse Y")
        );
        _rigidbody2D.MovePosition(_rigidbody2D.position + delta * _dragSpeed);
      }
    }
  }
}