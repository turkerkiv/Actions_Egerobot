using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickAnimationHandler : MonoBehaviour
{
    Animator _animator;
    int _upToDownHash;
    int _downToUpHash;
    bool _isUp;

    void Awake()
    {
        _animator = GetComponentInParent<Animator>();

        _upToDownHash = Animator.StringToHash("_upToDown");
        _downToUpHash = Animator.StringToHash("_downToUp");
    }

    public void PlayUpDownAnimation()
    {
        _isUp = !_isUp;
        _animator.SetTrigger(_isUp ? _upToDownHash : _downToUpHash);
    }
}
