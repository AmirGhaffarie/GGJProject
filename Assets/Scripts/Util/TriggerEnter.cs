using UnityEngine;
using UnityEngine.Events;

public class TriggerEnter : MonoBehaviour
{
    [SerializeField] bool checkTag;
    [SerializeField] string tagName;
    [SerializeField] UnityEvent<Collider> onTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (checkTag && !(other.CompareTag(tagName)))
            return;
        onTrigger.Invoke(other);
    }
}
