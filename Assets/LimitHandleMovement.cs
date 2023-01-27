using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitHandleMovement : MonoBehaviour
{
    [SerializeField] Transform _firstPoint;
    [SerializeField] Transform _player;

    float _maxDistance;
    Vector3 _direction;

    void Start()
    {
        _maxDistance = Vector3.Distance(_firstPoint.position, transform.position);
    }

    void Update()
    {
        // _direction = _player.position - _firstPoint.position;
        // _direction = Vector3.ClampMagnitude(_direction, _maxDistance - 0.5f);

        // transform.position = _firstPoint.position + _direction;
    }
}
