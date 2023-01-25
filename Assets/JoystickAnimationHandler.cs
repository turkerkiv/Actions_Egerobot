using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickAnimationHandler : MonoBehaviour
{
    [SerializeField] Transform _stickTip;
    [SerializeField] Vector3 _grabOffset;

    Animator _animator;

    int _upToDownHash;
    int _downToUpHash;
    bool _isUp;
    bool _isAnimationPlaying;

    Transform _handTarget;
    PlayerJoystickInteractionHandler _player;

    void Awake()
    {
        _animator = GetComponentInParent<Animator>();

        _upToDownHash = Animator.StringToHash("_upToDown");
        _downToUpHash = Animator.StringToHash("_downToUp");
    }

    void Update()
    {
        if (_isAnimationPlaying)
        {
            _handTarget.position = _stickTip.position + _grabOffset;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerRightHand") && !_isAnimationPlaying && _player != null)
        {
            _isAnimationPlaying = true;
            _isUp = !_isUp;
            _animator.SetTrigger(_isUp ? _upToDownHash : _downToUpHash);

            Debug.Log("Triggered");

            _player.CancelInvoke(nameof(_player.ResetWeight));
        }
    }

    public void PlayUpDownAnimation(Transform handTarget, PlayerJoystickInteractionHandler player)
    {
        _player = player;
        _handTarget = handTarget;
        _handTarget.position = _stickTip.position + _grabOffset;
    }

    void OnAnimationFinished()
    {
        _isAnimationPlaying = false;
        _handTarget = null;
        _player.ResetWeight();
        _player = null;
    }
}
