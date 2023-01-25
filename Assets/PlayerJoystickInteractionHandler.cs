using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerJoystickInteractionHandler : MonoBehaviour
{
    [SerializeField] InputAction _interact;
    [SerializeField] Transform _handIKTarget;
    [SerializeField] Rig _handIKRig;

    JoystickAnimationHandler _joystickAnimationHandler;

    float _handIKWeight;

    void OnEnable()
    {
        _interact.Enable();
    }

    void Update()
    {
        if (_joystickAnimationHandler != null && _interact.triggered)
        {
            _joystickAnimationHandler.PlayUpDownAnimation(_handIKTarget, this);

            _handIKWeight = 1f;

            CancelInvoke(nameof(ResetWeight));
            Invoke(nameof(ResetWeight), 1.0f);
        }

        _handIKRig.weight = Mathf.Lerp(_handIKRig.weight, _handIKWeight, Time.deltaTime * 5.0f);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out JoystickAnimationHandler joystickAnimationHandler))
        {
            _joystickAnimationHandler = joystickAnimationHandler;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_joystickAnimationHandler != null && other.TryGetComponent(out JoystickAnimationHandler joystickAnimationHandler))
        {
            _joystickAnimationHandler = null;
        }
    }

    void OnDisable()
    {
        _interact.Disable();
    }

    public void ResetWeight()
    {
        _handIKWeight = 0;
    }
}
