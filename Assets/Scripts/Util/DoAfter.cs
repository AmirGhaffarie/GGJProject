using UnityEngine;
using UnityEngine.Events;

public class DoAfter : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] UnityEvent thingsToDo;
    private void OnEnable()
    {
        Invoke(nameof(InvokeEvent), delay);
    }
    private void InvokeEvent()
    {
        thingsToDo.Invoke();
    }
}
