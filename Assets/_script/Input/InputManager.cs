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
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Dash.performed -= PerformDash;
    }

    #region Move
    public Vector2 MoveValue => _input.Player.Move.ReadValue<Vector2>();

    #endregion

    #region Look
    public Vector2 LookValue => _input.Player.Look.ReadValue<Vector2>();
    #endregion


    public delegate void DashPerformed();
    public DashPerformed OnDashPerformed;

    private void PerformDash(InputAction.CallbackContext context)
    {
        if (OnDashPerformed == null) return;
        OnDashPerformed();
    }
}
