using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    Vector3 offset;

    private void Awake()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
