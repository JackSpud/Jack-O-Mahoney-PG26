using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 10f;

    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        GetComponent<HitFlash>()?.Flash();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void ResetHealth() 
    {
        currentHealth = maxHealth;
    }

    void Die()
    {
        WaveSpawner.OnEnemyKilled();
        Destroy(gameObject);
    }
}
