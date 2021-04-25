using UnityEngine;

namespace Moth {
  public class MusicController : MonoBehaviour {
    [SerializeField] AudioSource _source;
    Brain _brain;
    WinTrigger _winTrigger;

    void Awake() {
      _brain = FindObjectOfType<Brain>();
      _winTrigger = FindObjectOfType<WinTrigger>();
    }

    void Update() {
      if (_winTrigger.DidWin && !_source.isPlaying) {
        _source.Play();
        return;
      }
      if (_brain.IsDead) {
        _source.Stop();
        return;
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