using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickAnimationHandler : MonoBehaviour
{
    [SerializeField] Transform _stickTip;
    [SerializeField] Vector3 _grabOffset;
    [SerializeField] ObjectMovement _objectToMove;
    [SerializeField] Outline _outline;

    Animator _animator;

    int _upToDownHash;
    int _downToUpHash;
    bool _isUp;
    bool _isAnimationPlaying;
    bool _isPlugged;

    Transform _handTarget;
    PlayerJoystickInteractionHandler _player;

    public Outline Outline => _outline;

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
            _player.CancelInvoke(nameof(_player.ResetWeight));

            if (_isPlugged)
                _objectToMove.Move(_isUp);
        }
    }

    public void PlayUpDownAnimation(Transform handTarget, PlayerJoystickInteractionHandler player, bool isPlugged)
    {
        _isPlugged = isPlugged;
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
