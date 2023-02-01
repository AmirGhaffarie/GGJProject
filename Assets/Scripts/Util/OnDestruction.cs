using UnityEngine;
using UnityEngine.Events;

public class OnDestruction : MonoBehaviour, IDestructible
{
    [SerializeField] UnityEvent onDestroy;
    [SerializeField] GameObject effect;
    [SerializeField] Transform target;

    public void Destroyed()
    {
        if (effect != null)
            Instantiate(effect, target.position, target.rotation);
        onDestroy.Invoke();
    }
}
