using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Base Stats")]
    public float baseDamage = 10f;
    public float baseHealth = 100f;
    public float baseAttackSpeed = 1f;
    public float baseMoveSpeed = 5f;

    [Header("Multipliers")]
    public float damageMultiplier = 1f;
    public float attackSpeedMultiplier = 1f;
    public float movementSpeedMultiplier = 1f;
    public float maxHealthMultiplier = 1f;

    public float currentHealth;

    void Start()
    {
        currentHealth = GetMaxHealth();
    }

    public float GetDamage()
    {
        return baseDamage * damageMultiplier;
    }

    public float GetMaxHealth()
    {
        return baseHealth * maxHealthMultiplier;
    }

    public void HealToFull()
    {
        currentHealth = GetMaxHealth();
    }
}