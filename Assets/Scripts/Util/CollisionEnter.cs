using UnityEngine;
using UnityEngine.Events;

public class CollisionEnter : MonoBehaviour
{
    [SerializeField] bool checkTag;
    [SerializeField] string tagName;
    [SerializeField] UnityEvent<Collision> onCollision;
    private void OnCollisionEnter(Collision collision)
    {
        if (checkTag && !(collision.gameObject.CompareTag(tagName)))
            return;
        onCollision.Invoke(collision);
    }
}
