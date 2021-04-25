using UnityEngine;

namespace Moth {
  public class MusicController : MonoBehaviour {
    [SerializeField] AudioSource _source;

    void Update() {
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