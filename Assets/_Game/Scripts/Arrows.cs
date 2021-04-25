using System;
using UnityEngine;

namespace Moth
{
  public class Arrows : MonoBehaviour
  {
    void Update() {
      if (Draggable.IsAnyDragging) Destroy(gameObject);
    }
  }
}