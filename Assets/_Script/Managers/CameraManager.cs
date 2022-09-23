using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _firstPersonCamera;
    [SerializeField] private CinemachineVirtualCamera _topDownCamera;
    [SerializeField] private List<CinemachineVirtualCamera> _allCinemachineCameras;
    [SerializeField] private Camera _brainCamera;

    [SerializeField] private PlayerMovement _player;

    private int _activeCameraValue = 15;
    private int _deactiveCameraValue = 1;


    private void Awake()
    {
        ActiveFristPerson();
    }

    private void OnEnable()
    {
        InputManager.Instance.OnChangeCam += ChangeCamera;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnChangeCam -= ChangeCamera;
    }

    private void ChangeCamera()
    {
        foreach(var cam in _allCinemachineCameras)
        {
            cam.Priority = cam.Priority == 1 ? 15 : 1;
        }
        if (_topDownCamera.Priority == _activeCameraValue) TopDownInputSettings();
        else
        {
            InputManager.Instance.EnableAllPlayerInputs();
            _brainCamera.orthographic = false;
        }
    }

    private void ActiveFristPerson()
    {
        foreach(var cam in _allCinemachineCameras)
        {
            cam.Priority = _deactiveCameraValue;
        }
        _firstPersonCamera.Priority = _activeCameraValue;
    }

    private void TopDownInputSettings()
    {
        InputManager.Instance.DisablePlayerLook();
        _player.transform.rotation = _player.StartPos.rotation;
        _brainCamera.orthographic = true;
    }
}
