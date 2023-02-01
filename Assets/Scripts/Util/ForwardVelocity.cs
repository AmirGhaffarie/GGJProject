using UnityEngine;

public class ForwardVelocity : MonoBehaviour
{
    [SerializeField] float speed;
    private void Start()
    {
        Vector3 velocity = transform.forward;
        velocity.y = 0;
        velocity = velocity.normalized * speed;
        GetComponent<Rigidbody>().velocity = velocity;
    }
    public void StopMoving()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
