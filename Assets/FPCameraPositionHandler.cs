using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCameraPositionHandler : MonoBehaviour
{
    [SerializeField] GameObject _fpCameraPosition;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fpCameraPosition = _fpCameraPosition.transform.position;
        transform.position = Vector3.Lerp(transform.position, fpCameraPosition, 0.1f);
    }
}
