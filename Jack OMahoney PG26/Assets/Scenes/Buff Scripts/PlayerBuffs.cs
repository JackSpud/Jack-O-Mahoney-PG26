using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffs : MonoBehaviour
{
    public List<BuffData> activeBuffs = new List<BuffData>();

    public PlayerStats playerStats;

    void Start()
    {
        if (playerStats == null)
            playerStats = GetComponent<PlayerStats>();
    }

    public void AddBuff(BuffData buff)
    {
        activeBuffs.Add(buff);

        ApplyBuffEffects(buff);

        Debug.Log("Added Buff: " + buff.buffName);
    }

    void ApplyBuffEffects(BuffData buff)
    {
        if (playerStats == null) return;

        // Damage buff
        playerStats.damageMultiplier += buff.bonusDamage;

        // Attack speed buff
        playerStats.attackSpeedMultiplier += buff.attackSpeedMultiplier;

        // Movement speed buff
        playerStats.movementSpeedMultiplier += buff.movementSpeedMultiplier;

        // Health buff
        if (buff.HealthIncrease > 0)
        {
            playerStats.maxHealthMultiplier += buff.HealthIncrease;
            playerStats.HealToFull();
            FindFirstObjectByType<PlayerHealthBar>()?.UpdateHealthUI();
        }
    }
}