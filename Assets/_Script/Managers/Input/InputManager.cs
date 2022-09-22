using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class InputManager : Singleton<InputManager>
{
    private GameInput _input;

    protected override void Awake()
    {
        base.Awake();
        _input = new GameInput();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Dash.performed += PerformDash;
        _input.Camera.ChangeCam.performed += PerformChangeCamera;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Dash.performed -= PerformDash;
        _input.Camera.ChangeCam.performed -= PerformChangeCamera;
    }

    #region Move
    public Vector2 MoveValue => _input.Player.Move.ReadValue<Vector2>();

    #endregion

    #region Look
    public Vector2 LookValue => _input.Player.Look.ReadValue<Vector2>();
    #endregion

    #region EnableInputs
    public void EnableAllPlayerInputs()
    {
        _input.Player.Enable();
    }

    public void EnableCameraInputs()
    {
        _input.Camera.Enable();
    }
    #endregion

    #region DisableInputs
    public void DisableAllPlayerInputs()
    {
        _input.Player.Disable();
    }

    public void DisablePlayerLook()
    {
        _input.Player.Look.Disable();
    }

    public void DisableCameraInputs()
    {
        _input.Camera.Disable();
    }
    #endregion

    public delegate void DashPerformed();
    public DashPerformed OnDashPerformed;

    private void PerformDash(InputAction.CallbackContext context)
    {
        if (OnDashPerformed == null) return;
        OnDashPerformed();
    }

    #region Camera
    public delegate void PerformChangeCam();
    public PerformChangeCam OnChangeCam;

    private void PerformChangeCamera(InputAction.CallbackContext context)
    {
        if (OnChangeCam == null) return;
        OnChangeCam();
    }
    #endregion
}
