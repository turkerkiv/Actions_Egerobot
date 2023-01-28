using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoConnectJoint : MonoBehaviour
{
    void Awake()
    {
        transform.GetComponent<ConfigurableJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
    }
}
