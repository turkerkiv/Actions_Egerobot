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
            joystickAnimationHandler.Outline.enabled = true;
        }

        if (_rope == null && other.TryGetComponent(out Interactable rope))
        {
            _rope = rope;
            rope.Outline.enabled = true;
            _rope.OnInteract += CarryRope;
        }

        if (_rope != null && _socket == null && other.TryGetComponent(out SocketBehaviour socket))
        {
            _socket = socket;
            socket.Outline.enabled = true;
            _socket.OnInteract += InteractSocket;
            Debug.Log("OnTriggerStay");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_joystickAnimationHandler != null && other.TryGetComponent(out JoystickAnimationHandler joystickAnimationHandler))
        {
            joystickAnimationHandler.Outline.enabled = false;
            _joystickAnimationHandler = null;
        }

        if (!_isCarrying && _rope != null && other.TryGetComponent(out Interactable interactable))
        {
            _rope.OnInteract -= CarryRope;
            _rope.Outline.enabled = false;
            _rope = null;
        }

        if (_socket != null && other.TryGetComponent(out SocketBehaviour socket))
        {
            _socket.OnInteract -= InteractSocket;
            Debug.Log("OnTriggerExit");
            _socket.Outline.enabled = false;
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
        Debug.Log("InteractSocketentry");
        if (!_isPlugging) return;
        Debug.Log("InteractSocketafter");

        _socket = socket;
        _socket.Outline.enabled = false;
        _socket.OnInteract -= InteractSocket;
        _isPlugging = false;
        _isPlugged = true;

        _isCarrying = false;
        _rope.Rigidbody.isKinematic = true;
        _rope.Outline.enabled = false;
        _rope.transform.position = new Vector3(-4.13000011f, 0.270000011f, -0.699999988f);
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
            _rope.transform.position = _rope.LimitHandleMovement.PlayerRightHand.position + _rope.Tip.forward * 0.05f;
            _rope.LimitHandleMovement.ClampPlayerMovement(_player);
            _rope.Rigidbody.isKinematic = true;

            _rope.Tip.rotation = Quaternion.LookRotation(_player.forward, Vector3.right);
            _rope.Tip.position = _rope.LimitHandleMovement.PlayerRightHand.position + _rope.Tip.forward * 0.15f;
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
