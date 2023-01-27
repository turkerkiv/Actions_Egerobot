using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerJoystickInteractionHandler : MonoBehaviour
{
    [SerializeField] InputAction _interactKey;
    [SerializeField] Transform _handIKTarget;
    [SerializeField] Rig _handIKRig;

    JoystickAnimationHandler _joystickAnimationHandler;
    Interactable _interactable;

    float _handIKWeight;
    bool _canCarry;

    void OnEnable()
    {
        _interactKey.Enable();
    }

    void Update()
    {
        if (_canCarry && _interactable != null)
        {
            _interactable.transform.position = _handIKTarget.position;
        }

        // if ((_joystickAnimationHandler != null && _interactKey.triggered))
        // {
        //     _joystickAnimationHandler.PlayUpDownAnimation(_handIKTarget, this);

        //     _handIKWeight = 1f;

        //     CancelInvoke(nameof(ResetWeight));
        //     Invoke(nameof(ResetWeight), 1.0f);
        // }

        if (_interactable != null && _interactKey.triggered && !_canCarry)
        {
            _handIKTarget.position = _interactable.transform.position;
            _handIKWeight = 1f;

            CancelInvoke(nameof(ResetWeight));
            Invoke(nameof(ResetWeight), 1.0f);
        }
        if (_interactable != null && _interactKey.triggered && _canCarry)
        {
            ResetWeight();
            _canCarry = false;
            _interactable = null;
        }

        _handIKRig.weight = Mathf.Lerp(_handIKRig.weight, _handIKWeight, Time.deltaTime * 5.0f);
    }

    void OnTriggerStay(Collider other)
    {
        // if (other.TryGetComponent(out JoystickAnimationHandler joystickAnimationHandler))
        // {
        //     _joystickAnimationHandler = joystickAnimationHandler;
        // }
        if (_interactable == null && other.TryGetComponent(out Interactable interactable))
        {
            _interactable = interactable;
            _interactable.OnInteract += Carry;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // if (_joystickAnimationHandler != null && other.TryGetComponent(out JoystickAnimationHandler joystickAnimationHandler))
        // {
        //     _joystickAnimationHandler = null;
        // }
        if (_interactable != null && other.TryGetComponent(out Interactable interactable))
        {
            _interactable = interactable;
        }
    }

    void OnDisable()
    {
        _interactKey.Disable();
    }

    void Carry(Interactable interactable)
    {
        _interactable = interactable;
        _interactable.OnInteract -= Carry;
        _canCarry = true;
        CancelInvoke(nameof(ResetWeight));
    }

    public void ResetWeight()
    {
        _handIKWeight = 0;
    }
}
