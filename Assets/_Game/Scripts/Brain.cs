﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Moth {
  public class Brain : MonoBehaviour
  {
    [SerializeField] GameObject _splat = null;
    public bool IsDead { get; private set; } = false;

    void Awake() {
      _splat.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) {
      if (IsDead) return;

      if (other.TryGetComponent<Moth>(out var moth)) {
        Destroy(moth);
        foreach (var r in moth.GetComponentsInChildren<Rigidbody2D>()) {
          r.simulated = false;
        }
      }
      _splat.SetActive(true);
      IsDead = true;
      StartCoroutine(RestartCoroutine());
    }

    IEnumerator RestartCoroutine() {
      yield return new WaitForSeconds(5f);
      SceneManager.LoadScene(0);
    }
  }
}