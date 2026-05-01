using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour,I_Health
{
    public float maxHealth = 10f;
    public float currentHealth;
    public GameObject damageNumberPrefab;
    private Animator anim;
    public enum EnemyState
    {
        Active,
        Dead
    }
    
    public EnemyState currentState = EnemyState.Active;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Active:
                break;

            case EnemyState.Dead:
                print("State is Dead");
                break;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Flash effect when hit
        GetComponent<HitFlash>()?.Flash();

        if (damageNumberPrefab != null)
        {
            Vector3 spawnPosition = transform.position + Vector3.up * 2f;

            GameObject dmg = Instantiate(damageNumberPrefab, spawnPosition, Quaternion.identity);

            DamageNumber dmgScript = dmg.GetComponent<DamageNumber>();
            if (dmgScript != null)
            {
                dmgScript.SetDamage(damage);
            }
        }

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

        if (CompareTag("Boss"))
        {
            // Clear the boss health bar if assigned
            if (WaveSpawner.instance != null && WaveSpawner.instance.bossHealthUI != null)
            {
                WaveSpawner.instance.bossHealthUI.ClearBoss();
            }
        }

        if (currentState == EnemyState.Dead) return;

        currentState = EnemyState.Dead;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = Vector3.zero;

        if (anim != null)
            anim.SetTrigger("Death");
        

        StartCoroutine(RunDeath());
    }

    IEnumerator RunDeath()
    {
        yield return new WaitForSeconds(2f);

        WaveSpawner.OnEnemyKilled(this);

        Destroy(gameObject);

    }

    internal void Hurt()
    {
        print("OW");
    }

    internal void Yay()
    {
        print("Yay!!");
    }
}