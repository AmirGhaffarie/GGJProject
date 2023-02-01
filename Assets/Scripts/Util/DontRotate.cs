using UnityEngine;

public class DontRotate : MonoBehaviour
{
    Quaternion rotation;
    private void OnEnable()
    {
        rotation = transform.rotation;
    }
    private void LateUpdate()
    {
        transform.rotation = rotation;
    }
}
