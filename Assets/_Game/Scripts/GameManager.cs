using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

namespace Moth {
  public class GameManager : MonoBehaviour {
    [SerializeField] ToggleVisibility _arrows = null;
    [SerializeField] CinemachineVirtualCamera _mothCamera = null;
    [SerializeField] CinemachineVirtualCamera _handleCamera = null;
    [SerializeField] CinemachineVirtualCamera _gameOverCamera = null;
    [SerializeField] Brain _brain = null;

    bool _hasStarted = false;

    void Start() {
      _brain.OnDeath += HandleBrainDeath;
    }

    void Update() {
      if (_hasStarted) {
        _handleCamera.gameObject.SetActive(!Draggable.IsAnyDragging);
      } else if (Draggable.IsAnyDragging) {
        _hasStarted = true;
        _arrows.IsVisible = false;
        _mothCamera.gameObject.SetActive(true);
      }

#if UNITY_STANDALONE
      if (Input.GetKeyDown(KeyCode.Escape)) {
        Application.Quit();
      }
#endif
      if (Input.GetKeyDown(KeyCode.R)) {
        SceneManager.LoadScene(0);
      }
    }

    void HandleBrainDeath() {
      _gameOverCamera.gameObject.SetActive(true);
      StartCoroutine(RestartCoroutine());
    }

    static IEnumerator RestartCoroutine() {
      yield return new WaitForSeconds(5f);
      SceneManager.LoadScene(0);
    }
  }
}