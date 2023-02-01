using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageDealer : MonoBehaviour
{
    public float damage;
    public float resetIntervals = 1;
    private List<Damagable> affectedObjects = new List<Damagable>();
    [SerializeField] UnityEvent onTrigger;
    private float timer = 0;
    public void ResetList()
    {
        affectedObjects.Clear();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > resetIntervals)
        {
            timer -= resetIntervals;
            ResetList();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTrigger.Invoke();
        var damagable = collision.GetComponent<Damagable>();
        if (damagable != null)
        {
            if (!affectedObjects.Contains(damagable))
            {
                affectedObjects.Add(damagable);
                damagable.TakeDamage(damage);
            }
        }
    }
}
