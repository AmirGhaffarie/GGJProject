using UnityEngine;
using UnityEngine.Events;

public class DoOnTouch : MonoBehaviour
{
    [SerializeField] private UnityEvent unityEvent;
    private void OnMouseDown()
    {
        unityEvent.Invoke();
    }
}
