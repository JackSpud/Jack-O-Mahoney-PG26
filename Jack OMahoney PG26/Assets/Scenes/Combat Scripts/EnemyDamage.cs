using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage = 1f;
    public float attackCooldown = 1f;
    public float attackRange = 1.5f;

    private float attackTimer;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        attackTimer -= Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange && attackTimer <= 0f)
        {
            Attack();
            attackTimer = attackCooldown;
        }
    }

    void Attack()
    {
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}
