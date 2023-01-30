using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class CameraModeChanger : MonoBehaviour
{
    [SerializeField] InputAction _changeCameraMode;
    [SerializeField] GameObject _fpCamera;
    [SerializeField] FirstPersonController _fpController;
    [SerializeField] GameObject _tpCamera;
    [SerializeField] ThirdPersonController _tpController;

    Outline[] _outlines;

    void Awake()
    {
        _outlines = FindObjectsOfType<Outline>();
    }

    void OnEnable()
    {
        _changeCameraMode.Enable();
    }

    void Start()
    {
        _fpCamera.SetActive(true);
        _fpController.enabled = true;

        _tpCamera.SetActive(false);
        _tpController.enabled = false;
    }

    void Update()
    {
        if (_changeCameraMode.triggered)
        {
            _fpCamera.SetActive(!_fpCamera.activeInHierarchy);
            _tpCamera.SetActive(!_tpCamera.activeInHierarchy);

            _fpController.enabled = !_fpController.enabled;
            _tpController.enabled = !_tpController.enabled;

            SetWidthsOfOutlines();
        }

        // if (_fpCamera.activeInHierarchy)
        // {
        //     _tpController.transform.position = _fpController.transform.position;
        //     _tpController.transform.rotation = _fpController.transform.rotation;
        // }
        // else
        // {
        //     _fpController.transform.position = _tpController.transform.position;
        //     _fpController.transform.rotation = _tpController.transform.rotation;
        // }
    }

    void OnDisable()
    {
        _changeCameraMode.Disable();
    }

    void SetWidthsOfOutlines()
    {
        if (_tpCamera.activeInHierarchy && _outlines[0].OutlineWidth > 3)
        {
            foreach (Outline outline in _outlines)
            {
                outline.OutlineWidth = 3;
            }
        }
        else if (_fpCamera.activeInHierarchy && _outlines[0].OutlineWidth < 10)
        {
            foreach (Outline outline in _outlines)
            {
                outline.OutlineWidth = 10;
            }
        }
    }

}
