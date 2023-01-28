using UnityEngine;

public class Interactable : MonoBehaviour
{
    public delegate void InteractEvent(Interactable interactable);
    public event InteractEvent OnInteract;

    Rigidbody _rigidbody;

    public LimitHandleMovement LimitHandleMovement { get; private set; }
    public Rigidbody Rigidbody => _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        LimitHandleMovement = GetComponent<LimitHandleMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerRightHand"))
        {
            LimitHandleMovement.PlayerRightHand = other.transform;
            OnInteract?.Invoke(this);
        }
    }
}