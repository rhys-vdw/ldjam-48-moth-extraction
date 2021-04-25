using System;
using UnityEngine;
using TMPro;

namespace Moth
{
  public class WinTrigger : MonoBehaviour
  {
    [SerializeField] TextMeshPro _winMessage = null;
    [SerializeField] TextMeshPro _title = null;

    public bool DidWin { get; private set; } = false;

    void Start() {
      _winMessage.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D c) {
      if (c.TryGetComponent<Moth>(out var moth)) {
        _winMessage.gameObject.SetActive(true);
        _title.gameObject.SetActive(false);
        DidWin = true;
        Destroy(gameObject);
      }
    }
  }
}