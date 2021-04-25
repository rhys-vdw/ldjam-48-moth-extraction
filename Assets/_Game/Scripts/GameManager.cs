using UnityEngine;
using UnityEngine.SceneManagement;

namespace Moth
{
  public class GameManager : MonoBehaviour
  {
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
  }
}