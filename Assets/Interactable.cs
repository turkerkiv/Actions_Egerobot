using UnityEngine;

public class Interactable : MonoBehaviour
{
    public delegate void InteractEvent(Interactable interactable);
    public event InteractEvent OnInteract;

    public LimitHandleMovement LimitHandleMovement { get; private set; }

    void Awake()
    {
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