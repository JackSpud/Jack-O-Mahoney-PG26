using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    public float currentHealth;

    [Header("Invincibility Frames")]
    public float invincibilityDuration = 1f;

    private bool isInvincible;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible)
            return;

        currentHealth -= damage;

        GetComponent<HitFlash>()?.Flash();

        if (currentHealth <= 0f)
        {
            Die();
            return;
        }

        StartCoroutine(InvincibilityFrames());
    }

    /// <summary>
    /// this just makes the player invincible waits for the duration
    /// and then turns the incinvincibility off.
    /// </summary>
    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    void Die()
    {
        Debug.Log("Player died");
        // Game over / respawn later
    }

}
