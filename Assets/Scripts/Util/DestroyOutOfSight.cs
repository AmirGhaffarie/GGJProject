using UnityEngine;

public class DestroyOutOfSight : MonoBehaviour
{
    [SerializeField] GameObject origin;
    private void OnBecameInvisible()
    {
        Destroy(origin);
    }
}
