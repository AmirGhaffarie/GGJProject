using UnityEngine;
using UnityEngine.Events;

public class FunctionToggle : MonoBehaviour
{
    [SerializeField] private UnityEvent toggleA;
    [SerializeField] private UnityEvent toggleB;

    bool toggled = false;

    public void DoToggle()
    {
        if (toggled)
            toggleB.Invoke();
        else
            toggleA.Invoke();
        toggled = !toggled;
    }
}
