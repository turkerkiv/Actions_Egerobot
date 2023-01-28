using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerJoystickInteractionHandler : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] InputAction _interactKey;
    [SerializeField] Transform _handIKTarget;
    [SerializeField] Rig _handIKRig;

    JoystickAnimationHandler _joystickAnimationHandler;
    Interactable _rope;
    SocketBehaviour _socket;

    float _handIKWeight;
    bool _isCarrying;
    bool _isPlugging;
    bool _isPlugged;

    void OnEnable()
    {
        _interactKey.Enable();
    }

    void Update()
    {
        HandleSocketInteraction();

        HandleRopeCarry();

        HandleRopeInteraction();

        HandleRopeDrop();

        HandleLeverInteraction();

        _handIKRig.weight = Mathf.Lerp(_handIKRig.weight, _handIKWeight, Time.deltaTime * 5.0f);
    }

    void OnTriggerStay(Collider other)
    {
        if (_joystickAnimationHandler == null && other.TryGetComponent(out JoystickAnimationHandler joystickAnimationHandler))
        {
            _joystickAnimationHandler = joystickAnimationHandler;
        }

        if (_rope == null && other.TryGetComponent(out Interactable rope))
        {
            _rope = rope;
            _rope.OnInteract += CarryRope;
        }

        if (_rope != null && _socket == null && other.TryGetComponent(out SocketBehaviour socket))
        {
            _socket = socket;
            socket.OnInteract += InteractSocket;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_joystickAnimationHandler != null && other.TryGetComponent(out JoystickAnimationHandler joystickAnimationHandler))
        {
            _joystickAnimationHandler = null;
        }

        if (!_isCarrying && _rope != null && other.TryGetComponent(out Interactable interactable))
        {
            _rope.OnInteract -= CarryRope;
            _rope = null;
        }

        if (_socket != null && other.TryGetComponent(out SocketBehaviour socket))
        {
            _socket.OnInteract -= InteractSocket;
            _socket = null;
        }
    }

    void OnDisable()
    {
        _interactKey.Disable();
    }

    public void ResetWeight()
    {
        _handIKWeight = 0;
    }

    void CarryRope(Interactable interactable)
    {
        _rope = interactable;
        _rope.OnInteract -= CarryRope;
        _isCarrying = true;
        CancelInvoke(nameof(ResetWeight));
    }

    void InteractSocket(SocketBehaviour socket)
    {
        Debug.Log("InteractSocket");

        _socket = socket;
        _socket.OnInteract -= InteractSocket;
        _isPlugging = false;
        _isPlugged = true;

        _isCarrying = false;
        _rope.Rigidbody.isKinematic = true;
        _rope.transform.position = _socket.transform.position;
        _rope = null;

        _handIKWeight = 0;

        CancelInvoke(nameof(ResetWeight));
    }

    void HandleRopeInteraction()
    {
        if (_rope != null && _interactKey.triggered && !_isCarrying)
        {
            _handIKTarget.position = _rope.transform.position;
            _handIKWeight = 1f;

            CancelInvoke(nameof(ResetWeight));
            Invoke(nameof(ResetWeight), 1.0f);
        }
    }

    void HandleRopeDrop()
    {
        if (!_isPlugging && _rope != null && _interactKey.triggered && _isCarrying)
        {
            ResetWeight();
            _isCarrying = false;
            _rope.Rigidbody.isKinematic = false;
            _rope = null;
        }
    }

    void HandleSocketInteraction()
    {
        if (!_isPlugging && _isCarrying && _rope != null && _socket != null && _interactKey.triggered)
        {
            Debug.Log("Plug in");

            _handIKTarget.position = _socket.transform.position;
            _handIKWeight = 1f;

            _isPlugging = true;

            CancelInvoke(nameof(ResetWeight));
            Invoke(nameof(ResetWeight), 1.0f);
        }
    }

    void HandleRopeCarry()
    {
        if (_isCarrying && _rope != null)
        {
            _rope.transform.position = _handIKTarget.position;
            _rope.LimitHandleMovement.GetClampedPosition(_player);
            _rope.Rigidbody.isKinematic = true;
        }
    }

    void HandleLeverInteraction()
    {
        if ((_joystickAnimationHandler != null && _interactKey.triggered))
        {
            _joystickAnimationHandler.PlayUpDownAnimation(_handIKTarget, this, _isPlugged);

            _handIKWeight = 1f;

            CancelInvoke(nameof(ResetWeight));
            Invoke(nameof(ResetWeight), 1.0f);
        }
    }


}
