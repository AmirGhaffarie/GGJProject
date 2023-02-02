using UnityEngine;
using UnityEngine.Events;

public class TriggerEnter2D : MonoBehaviour
{
    [SerializeField] bool checkTag;
    [SerializeField] string tagName;
    [SerializeField] UnityEvent<Collider2D> onTrigger;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (checkTag && !(other.CompareTag(tagName)))
            return;
        onTrigger.Invoke(other);
    }
}
