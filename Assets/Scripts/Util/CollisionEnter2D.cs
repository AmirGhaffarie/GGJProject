using UnityEngine;
using UnityEngine.Events;

public class CollisionEnter2D : MonoBehaviour
{
    [SerializeField] bool checkTag;
    [SerializeField] string tagName;
    [SerializeField] UnityEvent<Collision2D> onCollision;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (checkTag && !(collision.gameObject.CompareTag(tagName)))
            return;
        onCollision.Invoke(collision);
    }
}
