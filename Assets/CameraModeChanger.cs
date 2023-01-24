using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class CameraModeChanger : MonoBehaviour
{
    [SerializeField] GameObject _fpCamera;
    [SerializeField] GameObject _fpPlayerObject;
    [SerializeField] GameObject _tpCamera;
    [SerializeField] GameObject _tpPlayerObject;

    void Start()
    {
        _fpCamera.SetActive(true);
        _tpCamera.SetActive(!_fpCamera.activeInHierarchy);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _fpCamera.SetActive(!_fpCamera.activeInHierarchy);
            _tpCamera.SetActive(!_tpCamera.activeInHierarchy);
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
}
