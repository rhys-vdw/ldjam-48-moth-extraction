using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

namespace Moth {
  public class GameManager : MonoBehaviour {
    enum GameState {
      Title,
      Game,
      SweetRelief,
      BrainDeath
    }

    [SerializeField] CinemachineVirtualCamera _mothCamera = null;
    [SerializeField] CinemachineVirtualCamera _handleCamera = null;
    [SerializeField] CinemachineVirtualCamera _gameOverCamera = null;
    [SerializeField] Brain _brain = null;
    [SerializeField] WinTrigger _winTrigger = null;
    [SerializeField] GameObject[] _deactivateOnStartGame = null;
    [SerializeField] GameObject[] _activateOnWin = null;
    [SerializeField] GameObject[] _deactivateOnWin = null;
    [SerializeField] AudioSource _musicSource = null;
    [SerializeField] Draggable _dragHandle = null;
    [SerializeField] Moth _moth = null;

    GameState _gameState = GameState.Title;

    void Start() {
      _brain.OnDeath += HandleBrainDeath;
      _winTrigger.OnWin += HandleWin;
      _dragHandle.OnDragStart += HandleDragStart;
      _dragHandle.OnDragEnd += HandleDragEnd;

      SetActive(_activateOnWin, false);
    }

    void Update() {
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
      _gameState = GameState.BrainDeath;
      _gameOverCamera.gameObject.SetActive(true);
      StartCoroutine(RestartCoroutine());
    }

    void HandleWin() {
      _gameState = GameState.SweetRelief;

      _moth.IsWiggling = false;
      _moth.IsFlapping = true;

      _mothCamera.gameObject.SetActive(false);
      _handleCamera.gameObject.SetActive(false);

      SetActive(_activateOnWin, true);
      SetActive(_deactivateOnWin, false);
    }

    void HandleDragStart() {
      if (_gameState == GameState.Title) {
        _gameState = GameState.Game;
        _mothCamera.gameObject.SetActive(true);
        SetActive(_deactivateOnStartGame, false);
        _moth.IsFlapping = true;
        _moth.IsWiggling = true;
      }

      if (_gameState == GameState.Game) {
        _musicSource.Play();
        _handleCamera.gameObject.SetActive(false);
      }
    }

    void HandleDragEnd() {
      if (_gameState == GameState.Game) {
        _musicSource.Pause();
        _handleCamera.gameObject.SetActive(true);
      }
    }

    void SetActive(GameObject[] gameObjects, bool isActive) {
      foreach (var gameObject in gameObjects) {
        gameObject.SetActive(isActive);
      }
    }

    static IEnumerator RestartCoroutine() {
      yield return new WaitForSeconds(5f);
      SceneManager.LoadScene(0);
    }
  }
}