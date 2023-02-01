using UnityEngine;
using UnityEngine.Events;

public class CollisionStay : MonoBehaviour
{
    private const string fillAmount = "_FillAmount";
    [SerializeField] float timeNeeded;
    [SerializeField] LayerMask layer;
    [SerializeField] UnityEvent onStayedEnoughTime;
    private float timer = 0;

    MaterialPropertyBlock materialProperties;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        materialProperties = new MaterialPropertyBlock();
        materialProperties.SetFloat(fillAmount, 0);
        materialProperties.SetTexture("_MainTex", spriteRenderer.sprite.texture);
        spriteRenderer.SetPropertyBlock(materialProperties);
    }

    private void UpdateVisual()
    {
        materialProperties.SetFloat(fillAmount, Mathf.Clamp01(timer / timeNeeded));
        spriteRenderer.SetPropertyBlock(materialProperties);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (layer.Contains(collision.gameObject.layer))
        {
            timer += Time.deltaTime;
            UpdateVisual();
            if (timer >= timeNeeded)
            {
                timer -= timeNeeded;
                onStayedEnoughTime.Invoke();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (layer.Contains(collision.gameObject.layer))
        {
            timer += Time.deltaTime;
            UpdateVisual();
            if (timer >= timeNeeded)
            {
                timer -= timeNeeded;
                onStayedEnoughTime.Invoke();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (layer.Contains(collision.gameObject.layer))
        {
            timer = 0;
            UpdateVisual();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (layer.Contains(collision.gameObject.layer))
        {
            timer = 0;
            UpdateVisual();
        }
    }
}
