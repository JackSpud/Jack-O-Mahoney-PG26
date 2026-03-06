using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    PlayerStats playerStats;

    public float maxHealth = 10f;
    public float currentHealth;

    [Header("Invincibility Frames")]
    public float invincibilityDuration = 1f;

    private bool isInvincible;

    PlayerHealthBar healthUI;

    void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
        healthUI = FindFirstObjectByType<PlayerHealthBar>();

        if (playerStats != null)
            currentHealth = playerStats.GetMaxHealth();
        else
            currentHealth = maxHealth;

        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible)
            return;

        currentHealth -= damage;

        GetComponent<HitFlash>()?.Flash();

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthUI();

        if (currentHealth <= 0f)
        {
            Die();
            return;
        }

        StartCoroutine(InvincibilityFrames());
    }

    void UpdateHealthUI()
    {
        if (healthUI == null)
            healthUI = FindFirstObjectByType<PlayerHealthBar>();

        if (healthUI != null)
            healthUI.UpdateHealthUI();
    }
    /// <summary>
    /// this just makes the player invincible for a short time after taking damage, to prevent them from taking multiple hits in quick succession
    /// </summary>
    /// <returns></returns>
    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    void Die()
    {
        Debug.Log("Player died");
    }
}