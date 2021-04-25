using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class JointMotor2DUtility {
  public static JointMotor2D WithSpeed(this JointMotor2D motor, float speed) => new JointMotor2D {
    motorSpeed = speed,
    maxMotorTorque = motor.maxMotorTorque
  };
}

public class Moth : MonoBehaviour {
  [Header("References")]
  [SerializeField] HingeJoint2D[] _bodyJoints = null;
  [SerializeField] HingeJoint2D _leftWingJoint = null;
  [SerializeField] HingeJoint2D _rightWingJoint = null;
  [SerializeField] Transform _rightWingScaler = null;
  [SerializeField] Transform _leftWingScaler = null;

  [Header("Flapping")]
  [SerializeField] float _flapFrequency = 1f;
  [SerializeField] float _flapSpeed = 360f;
  [SerializeField] float _minScale = 0.2f;

  public bool IsFlapping = false;

  void FixedUpdate() {
    _leftWingJoint.useMotor = IsFlapping;
    _rightWingJoint.useMotor = IsFlapping;
    if (IsFlapping) {
      var speed = _flapSpeed * Mathf.Sin(_flapFrequency * Time.time);
      _leftWingJoint.motor = _leftWingJoint.motor.WithSpeed(speed);
      _rightWingJoint.motor = _rightWingJoint.motor.WithSpeed(-speed);
    }

    {
      var distance = _leftWingJoint.jointAngle - _leftWingJoint.limits.min;
      _leftWingScaler.localScale = new Vector3(1, Mathf.Clamp(distance / 90f, _minScale, 1), 1);
    }
    {
      var distance = _rightWingJoint.limits.max - _rightWingJoint.jointAngle;
      _rightWingScaler.localScale = new Vector3(1, Mathf.Clamp(distance / 90f, _minScale, 1), 1);
    }
  }
}