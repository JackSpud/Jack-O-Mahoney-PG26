using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public float attackInterval = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;

    private float attackTimer;

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            Attack();
            attackTimer = 0f;
        }
    }

    void Attack()
    {
        Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );
    }
}
