using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitHandleMovement : MonoBehaviour
{
    [SerializeField] Transform _firstPoint;

    float _maxDistance;

    // ConfigurableJoint _joint;

    public Transform PlayerRightHand { get; set; }

    void Awake()
    {
        // _joint = GetComponent<ConfigurableJoint>();
    }

    void Start()
    {
        _maxDistance = Vector3.Distance(_firstPoint.position, transform.position);
        transform.position = _firstPoint.position;

        // _joint.connectedBody = _ropeGenerator.LastSegment.GetComponent<Rigidbody>();
        // transform.position = _ropeGenerator.LastSegment.position;
    }

    public void GetClampedPosition(Transform player)
    {
        Vector3 dirMag = PlayerRightHand.position - _firstPoint.position;
        Vector3 armBodyOffset = player.position - PlayerRightHand.position;
        dirMag = Vector3.ClampMagnitude(dirMag, _maxDistance);
        player.position = _firstPoint.position + dirMag + armBodyOffset;
    }
}
