using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class CameraModeChanger : MonoBehaviour
{
    [SerializeField] InputAction _changeCameraMode;
    [SerializeField] GameObject _fpCamera;
    [SerializeField] GameObject _fpPlayerObject;
    [SerializeField] GameObject _tpCamera;
    [SerializeField] GameObject _tpPlayerObject;

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
        _tpCamera.SetActive(!_fpCamera.activeInHierarchy);
    }

    void Update()
    {
        if (_changeCameraMode.triggered)
        {
            _fpCamera.SetActive(!_fpCamera.activeInHierarchy);
            _tpCamera.SetActive(!_tpCamera.activeInHierarchy);

            SetWidthsOfOutlines();
        }

        if (_fpCamera.activeInHierarchy)
        {
            _tpPlayerObject.transform.position = _fpPlayerObject.transform.position;
            _tpPlayerObject.transform.rotation = _fpPlayerObject.transform.rotation;
        }
        else
        {
            _fpPlayerObject.transform.position = _tpPlayerObject.transform.position;
            _fpPlayerObject.transform.rotation = _tpPlayerObject.transform.rotation;
        }
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
