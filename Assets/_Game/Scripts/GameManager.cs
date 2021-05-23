using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

namespace Moth {
  public class GameManager : MonoBehaviour {
    enum GameState {
      Title,
      Game,
      SweetRelief,
      BrainDeath
    }

    [Header("References")]
    [SerializeField] Moth _moth = null;
    [SerializeField] Brain _brain = null;
    [SerializeField] WinTrigger _winTrigger = null;
    [SerializeField] Draggable _dragHandle = null;
    [SerializeField] GameObject[] _deactivateOnStartGame = null;
    [SerializeField] GameObject[] _activateOnWin = null;
    [SerializeField] GameObject[] _deactivateOnWin = null;
    [SerializeField] TextMeshPro _versionText = null;

    [Header("Cameras")]
    [SerializeField] CinemachineVirtualCamera _mothCamera = null;
    [SerializeField] CinemachineVirtualCamera _handleCamera = null;
    [SerializeField] CinemachineVirtualCamera _gameOverCamera = null;

    [Header("Audio")]
    [SerializeField] AudioSource _musicSource = null;
    [SerializeField] float _musicFadeSpeed = 10f;

    GameState _gameState = GameState.Title;
    bool _isMusicPlaying = false;
    float _musicVolume;

    void Start() {
      _brain.OnDeath += HandleBrainDeath;
      _winTrigger.OnWin += HandleWin;
      _dragHandle.OnDragStart += HandleDragStart;
      _dragHandle.OnDragEnd += HandleDragEnd;
      _musicVolume = _musicSource.volume;
      _musicSource.volume = 0;
      _musicSource.Stop();

      _versionText.text = _versionText.text.Replace("[VERSION]", Application.version);

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

      _musicSource.volume = Mathf.MoveTowards(
        _musicSource.volume,
        _isMusicPlaying ? _musicVolume : 0,
        _musicFadeSpeed * Time.deltaTime
      );
      if (_musicSource.volume == 0) {
        if (_musicSource.isPlaying) _musicSource.Pause();
      } else if (!_musicSource.isPlaying) {
        _musicSource.Play();
      }
    }

    void HandleBrainDeath() {
      _gameState = GameState.BrainDeath;
      _gameOverCamera.gameObject.SetActive(true);

      _isMusicPlaying = false;

      StartCoroutine(RestartCoroutine());
    }

    void HandleWin() {
      _gameState = GameState.SweetRelief;

      _moth.IsWiggling = false;
      _moth.IsFlapping = true;

      _mothCamera.gameObject.SetActive(false);
      _handleCamera.gameObject.SetActive(false);

      _isMusicPlaying = true;

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
        _isMusicPlaying = true;
        _handleCamera.gameObject.SetActive(false);
      }
    }

    void HandleDragEnd() {
      if (_gameState == GameState.Game) {
        _isMusicPlaying = false;
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