using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 10f;

    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Flash effect when hit
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
        // Notify wave spawner that this enemy is dead
        WaveSpawner.OnEnemyKilled();

        if (CompareTag("Boss"))
        {
            // Clear the boss health bar if assigned
            if (WaveSpawner.instance != null && WaveSpawner.instance.bossHealthUI != null)
            {
                WaveSpawner.instance.bossHealthUI.ClearBoss();
            }
        }

        Destroy(gameObject);
    }
}