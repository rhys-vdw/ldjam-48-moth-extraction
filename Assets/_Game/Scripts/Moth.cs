using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moth : MonoBehaviour {
  [Header("References")]
  [SerializeField] HingeJoint2D[] _bodyJoints;
  [SerializeField] HingeJoint2D _leftWingJoint;
  [SerializeField] HingeJoint2D _rightWingJoint;

  [Header("Flapping")]
  [SerializeField] float _flapInterval = 0.2f;
  [SerializeField] float _flapSpeed = 360f;
  [SerializeField] float _flapTorque = 20f;

  public bool IsFlapping = false;
  float _nextFlapDirectionChangeTime;

  void FixedUpdate() {
    _leftWingJoint.useMotor = IsFlapping;
    _rightWingJoint.useMotor = IsFlapping;
    if (IsFlapping) {
      if (Time.time > _nextFlapDirectionChangeTime) {
        _nextFlapDirectionChangeTime = Time.time + _flapInterval;
        var flapSpeed = _leftWingJoint.motor.motorSpeed < 0
          ? _flapSpeed
          : -_flapSpeed;
        _leftWingJoint.motor = new JointMotor2D {
          motorSpeed = flapSpeed,
          maxMotorTorque = _flapTorque
        };
        _rightWingJoint.motor = new JointMotor2D {
          motorSpeed = -flapSpeed,
          maxMotorTorque = _flapTorque
        };
      }
    }
  }
}