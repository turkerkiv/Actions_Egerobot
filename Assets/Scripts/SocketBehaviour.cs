using UnityEngine;

public class SocketBehaviour : MonoBehaviour
{
    [SerializeField] Outline _outline;

    public delegate void InteractSocketEvent(SocketBehaviour socketBehaviour);
    public event InteractSocketEvent OnInteract;

    public Outline Outline => _outline;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerRightHand"))
        {
            Debug.Log("SocketBehaviour.OnTriggerEnter");
            OnInteract?.Invoke(this);
        }
    }
}