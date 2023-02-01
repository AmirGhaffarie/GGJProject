using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    [SerializeField] GameObject target;
    public void Destroy()
    {
        Destroy(gameObject);
    }
    public void DestroyTarget()
    {
        if (target != null)
            Destroy(target);
    }
}
