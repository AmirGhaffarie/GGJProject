using UnityEngine;
using UnityEngine.Events;

public class MenuButton : MonoBehaviour
{
    [SerializeField] UnityEvent onDeselect;
    [SerializeField] UnityEvent onSelect;
    [SerializeField] UnityEvent onSubmit;
    [SerializeField] UnityEvent onRight;
    [SerializeField] UnityEvent onLeft;
    public void OnDeselect()
    {
        onDeselect.Invoke();
    }

    public void OnSelect()
    {
        onSelect.Invoke();
    }

    public void OnSubmit()
    {
        onSubmit.Invoke();
    }
    public void OnRight()
    {
        onRight.Invoke();
    }
    public void OnLeft()
    {
        onLeft.Invoke();
    }
}
