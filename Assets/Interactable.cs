using UnityEngine;

public class Interactable : MonoBehaviour
{
    public delegate void InteractEvent(Interactable interactable);
    public event InteractEvent OnInteract;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerRightHand"))
        {
            OnInteract?.Invoke(this);
        }
    }
}