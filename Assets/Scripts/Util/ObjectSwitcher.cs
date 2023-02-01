using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    [SerializeField] GameObject[] target;
    public void EnableObject(int index = 0)
    {
        target[index].SetActive(true);
    }
    public void DisableObject(int index = 0)
    {
        target[index].SetActive(false);
    }
}
