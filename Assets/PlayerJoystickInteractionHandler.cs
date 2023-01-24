using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystickInteractionHandler : MonoBehaviour
{
    [SerializeField] float _interactionRadius = 5.0f;

    JoystickAnimationHandler _joystickAnimationHandler;

    void Start()
    {

    }

    void Update()
    {
        var colliders = Physics.OverlapSphere(transform.position, _interactionRadius);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out _joystickAnimationHandler))
            {
                break;
            }
        }

        if (_joystickAnimationHandler != null && Input.GetKeyDown(KeyCode.E))
        {
            _joystickAnimationHandler.PlayUpDownAnimation();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _interactionRadius);
    }
}
