using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] Transform _startPoint;
    [SerializeField] Transform _endPoint;
    [SerializeField] float _speed = 5f;

    Transform _currentTarget;

    void Update()
    {
        if (_currentTarget == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget.position, _speed * Time.deltaTime);

        if (transform.position == _currentTarget.position)
            _currentTarget = null;
    }

    public void Move(bool moveToEnd)
    {
        if (moveToEnd)
            _currentTarget = _endPoint;
        else
            _currentTarget = _startPoint;
    }
}
