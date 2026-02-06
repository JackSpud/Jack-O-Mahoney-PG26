using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float rotateSpeed = 20f;
    public float lifeTime = 5f;
    public float hitDistance = 0.5f;
    public float damage = 1f;

    private Transform target;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, target.position);

        // If we're close enough, apply damage + knockback
        if (distance <= hitDistance)
        {
            HitTarget();
            return;
        }

        // Smoothly rotate towards target
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void HitTarget()
    {
        if (target == null) return;

        // Damage
        EnemyHealth health = target.GetComponent<EnemyHealth>();
        if (health != null)
            health.TakeDamage(damage);

        // Knockback
        EnemyKnockback knockback = target.GetComponent<EnemyKnockback>();
        PlayerKnockback playerKnockback = PlayerKnockback.FindFirstObjectByType<PlayerKnockback>();
        if (knockback != null && playerKnockback != null)
        {
            knockback.ApplyKnockback(transform.position, playerKnockback.GetKnockback());
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && other.transform == target)
        {
            HitTarget();
        }
    }
}
