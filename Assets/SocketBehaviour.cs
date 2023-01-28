using UnityEngine;

public class SocketBehaviour : MonoBehaviour
{
    public delegate void InteractEvent(SocketBehaviour socketBehaviour);
    public event InteractEvent OnInteract;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerRightHand"))
        {
            OnInteract?.Invoke(this);
        }
    }
}