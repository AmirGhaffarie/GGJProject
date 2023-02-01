using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public void StartTimer(float time = 1)
    {
        Destroy(gameObject, time);
    }
}
