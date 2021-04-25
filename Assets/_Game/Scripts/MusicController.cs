using UnityEngine;

namespace Moth {
  public class MusicController : MonoBehaviour {
    [SerializeField] AudioSource _source;
    Brain _brain;

    void Awake() {
      _brain = FindObjectOfType<Brain>();
    }

    void Update() {
      if (_brain.IsDead) {
        _source.Stop();
      }
      if (Draggable.IsAnyDragging) {
        if (!_source.isPlaying) {
          _source.Play();
        }
      } else {
        if (_source.isPlaying) {
          _source.Pause();
        }
      }
    }
  }
}