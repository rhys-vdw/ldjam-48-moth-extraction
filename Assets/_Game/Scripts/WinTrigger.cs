using UnityEngine;
using System;

namespace Moth {
  public class WinTrigger : MonoBehaviour {
    public Action OnWin;

    void OnTriggerEnter2D(Collider2D c) {
      if (c.TryGetComponent<Moth>(out var moth)) {
        OnWin();
        Destroy(gameObject);
      }
    }
  }
}