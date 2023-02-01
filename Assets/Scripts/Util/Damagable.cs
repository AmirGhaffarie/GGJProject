using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    [SerializeField] bool usingStats;
    [SerializeField] float baseHealth;

    [SerializeField] bool usingExternalVars;
    [SerializeField] FloatVariable health;
    [SerializeField] FloatVariable percentHealth;

    [SerializeField] UnityEvent<float> onDamaged;
    [SerializeField] UnityEvent onDeath;
    private float currentHealth;

    private void Start()
    {
        currentHealth = baseHealth;
        if (usingExternalVars)
            UpdateHealthValues();
    }

    private void UpdateHealthValues()
    {
        health.Value = currentHealth;
        percentHealth.Value = currentHealth / baseHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (usingExternalVars)
            UpdateHealthValues();
        onDamaged.Invoke(currentHealth);
        if (currentHealth < 0 && enabled)
        {
            enabled = false;
            onDeath.Invoke();
        }
    }
}
