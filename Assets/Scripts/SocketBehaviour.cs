using UnityEngine;

public class SocketBehaviour : MonoBehaviour
{
    [SerializeField] Outline _outline;

    public delegate void InteractEvent(SocketBehaviour socketBehaviour);
    public event InteractEvent OnInteract;

    public Outline Outline => _outline;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerRightHand"))
        {
            OnInteract?.Invoke(this);
        }
    }
}