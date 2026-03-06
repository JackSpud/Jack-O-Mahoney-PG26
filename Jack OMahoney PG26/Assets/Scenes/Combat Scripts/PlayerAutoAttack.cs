using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    PlayerStats playerStats;
    public float attackInterval = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float attackRange = 15f;

    private float attackTimer;

    void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        float attackSpeedMultiplier = playerStats != null
            ? playerStats.attackSpeedMultiplier
            : 1f;

        float adjustedInterval = attackInterval / Mathf.Max(attackSpeedMultiplier, 0.1f);

        if (attackTimer >= adjustedInterval)
        {
            Transform target = FindNearestEnemy();

            if (target != null)
            {
                Attack(target);
                attackTimer = 0f;
            }
        }
    }

    void Attack(Transform target)
    {
        // Calculate direction from fire point to enemy
        Vector3 directionToTarget = (target.position - firePoint.position).normalized;

        // Optional: keep projectile horizontal only
        directionToTarget.y = 0f;

        // Create rotation looking at the enemy
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);

        // Spawn projectile facing the target
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, lookRotation);

        // Set homing target
        Projectile homing = projectile.GetComponent<Projectile>();
        if (homing != null)
        {
            homing.SetTarget(target);
        }

    }

    Transform FindNearestEnemy()
    {
        EnemyHealth[] enemies = FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);

        Transform closest = null;
        float closestDistance = attackRange;

        foreach (EnemyHealth enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy.transform;
            }
        }

        return closest;
    }
}
