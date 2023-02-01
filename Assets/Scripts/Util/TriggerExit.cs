using UnityEngine;
using UnityEngine.Events;

public class TriggerExit : MonoBehaviour
{
    [SerializeField] bool checkTag;
    [SerializeField] string tagName;
    [SerializeField] UnityEvent<Collider> onTrigger;
    private void OnTriggerExit(Collider other)
    {
        if (checkTag && !(other.CompareTag(tagName)))
            return;
        onTrigger.Invoke(other);
    }
}
