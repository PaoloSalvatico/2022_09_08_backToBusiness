using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _dashForce;

    private Vector3 _moveDirection;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnDash()
    {
        Dash();
    }

    private void Dash()
    {
        if (_moveDirection == Vector3.zero)
        {
            _rigidbody.AddRelativeForce(-transform.GetLocalForward() * _dashForce, ForceMode.Impulse);
        }
        else
        {
            _rigidbody.AddRelativeForce(_moveDirection * _dashForce, ForceMode.Impulse);
        }
    }
}
