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
        hitDistance = speed * Time.deltaTime + 1.2f;
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

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= hitDistance)
        {
            EnemyHealth health = target.GetComponent<EnemyHealth>();
            if (health != null)
                health.TakeDamage(damage);

            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        /// Allows the projectile to smoothly rotate/Curve towards the target
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
        );

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth health = other.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }

}
