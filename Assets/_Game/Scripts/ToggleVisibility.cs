using System;
using UnityEngine;

namespace Moth
{
  public class ToggleVisibility : MonoBehaviour {
    public bool IsVisible {
      get => gameObject.activeSelf;
      set => gameObject.SetActive(value);
    }
  }
}