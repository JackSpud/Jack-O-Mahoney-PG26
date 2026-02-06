using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning($"{name} is missing a Rigidbody for knockback!");
        }
    }

    // Call this whenever the enemy is hit
    public void ApplyKnockback(Vector3 sourcePosition, float knockbackAmount)
    {
        if (rb == null) return;

        Vector3 direction = (transform.position - sourcePosition).normalized;
        direction.y = 0f; // optional: keep knockback horizontal

        rb.AddForce(direction * knockbackAmount, ForceMode.Impulse);
    }
}
