using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _baseMoveSpeed = 20;
    [SerializeField] private float _currentMoveSpeed = 20;
    [SerializeField] private float _inAirMoveSpeed = 10;
    [SerializeField] private float _playerBaseMass = 1;
    [SerializeField] private float _playerFallingMass = 3;

    [Header("Rotation")]
    [SerializeField] private float _rotationSpeedX = 120;

    [Header("Dash")]
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashCooldown = 2f;

    [Header("Collision Management")]
    [SerializeField] private LayerMask _collisionLayers;
    [Tooltip("How much collision sensor distance add to capsule radius")]
    [SerializeField] private float _sensorDistance;
    [SerializeField] private float _offset = .5f;
    [SerializeField] private float _botOffset = .25f;

    private Vector3 _moveDirection;
    private Vector3 _moveAmount;
    private Vector3 _smoothMoveVelocity;
    private Vector3 _eulerAngleVelocity;
    private Vector3 _localMove;

    private Vector3 _sensorOffset;
    private Vector3 _botSensorOffset;

    private Rigidbody _rigidbody;

    private bool _isDashing;
    private bool _isDashInCooldown;

    private bool _topCollisionCheck;
    private bool _midTopCollisionCheck;
    private bool _midCollisionCheck;
    private bool _midBotCollisionCheck;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _sensorOffset = new Vector3(0f, _offset, 0f);
        _botSensorOffset = new Vector3(0f, _botOffset, 0f);
    }

    private void OnEnable()
    {
        InputManager.Instance.OnDashPerformed += OnDash;
    }

    private void Update()
    {
        float inputX = InputManager.Instance.MoveValue.x;
        float inputY = InputManager.Instance.MoveValue.y;

        // Calculate rotation
        float inputRotationX = InputManager.Instance.LookValue.x;

        _eulerAngleVelocity = new Vector3(0f, inputRotationX * _rotationSpeedX, 0f);

        _moveDirection = new Vector3(inputX, 0f, inputY).normalized;

        Vector3 targetMoveAmount = _moveDirection * _currentMoveSpeed;
        _moveAmount = Vector3.SmoothDamp(_moveAmount, targetMoveAmount, ref _smoothMoveVelocity, .15f);


    }

    private void FixedUpdate()
    {
        #region Sensors
        // Top Sensor
        ShootSensor(transform.position + transform.up, ref _topCollisionCheck);
        // Middle-Top Sensor
        ShootSensor(transform.position + _sensorOffset, ref _midTopCollisionCheck);
        // Middle Sensor
        ShootSensor(transform.position, ref _midCollisionCheck);
        // Middle-Bottom Sensor
        ShootSensor(transform.position - _botSensorOffset, ref _midBotCollisionCheck);
        #endregion

        // Apply movement to rigidbody
        _localMove = transform.TransformDirection(_moveAmount) * Time.fixedDeltaTime;

        // Apply movement if not colliding
        if (!IsColliding() && !_isDashing) _rigidbody.MovePosition(_rigidbody.position + _localMove);

        // Rotate rigidbody
        Quaternion newRotation = Quaternion.Euler(_eulerAngleVelocity * Time.fixedDeltaTime);
        _rigidbody.MoveRotation(_rigidbody.rotation * newRotation);
    }

    /// <summary>
    /// Is player colliding
    /// </summary>
    /// <returns>True if collides</returns>
    private bool IsColliding()
    {
        return _topCollisionCheck || _midTopCollisionCheck || _midCollisionCheck || _midBotCollisionCheck;
    }

    public void OnDash()
    {
        if(!_isDashing) Dash();
    }

    private void Dash()
    {
        if (_moveDirection == Vector3.zero)
        {
            _rigidbody.AddRelativeForce(-transform.forward * _dashForce, ForceMode.Impulse);
        }
        else
        {
            _rigidbody.AddRelativeForce(_moveDirection * _dashForce, ForceMode.Impulse);
        }

        _isDashing = true;
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        _isDashInCooldown = true;
        yield return new WaitForSeconds(_dashCooldown);
        _isDashInCooldown = false;
        _isDashing = false;
    }

    private void ShootSensor(Vector3 startPosition, ref bool collCheck)
    {
        RaycastHit hit;
        if (Physics.Raycast(startPosition, _localMove, out hit, _sensorDistance, _collisionLayers))
        {
            collCheck = true;
#if UNITY_EDITOR
            Debug.DrawRay(startPosition, _sensorDistance * _localMove, Color.red);
#endif
        }
        else
        {
            collCheck = false;
#if UNITY_EDITOR
            Debug.DrawRay(startPosition, _sensorDistance * _localMove, Color.green);
#endif
        }
    }
}
