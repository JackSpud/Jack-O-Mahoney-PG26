using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    [Header("Knockback Settings")]
    public float baseKnockback = 5f; // default pushback
    public float knockbackMultiplier = 1f; // can be upgraded later

    // Returns the final knockback amount
    public float GetKnockback()
    {
        return baseKnockback * knockbackMultiplier;
    }
}
