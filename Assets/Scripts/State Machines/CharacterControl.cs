using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public Rigidbody2D rb2d;


    [HideInInspector] public float ElapsedStateTime;

    private void Update()
    {
        SetOrientation();
        ElapsedStateTime += Time.deltaTime;
    }

    private void SetOrientation()
    {
        if (rb2d.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (rb2d.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        animator = animator == null ? GetComponent<Animator>() : animator;
        rb2d = rb2d == null ? GetComponent<Rigidbody2D>() : rb2d;
    }
#endif
}