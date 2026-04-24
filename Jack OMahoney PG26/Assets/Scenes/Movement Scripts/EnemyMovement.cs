using UnityEngine;
using static EnemyHealth;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;           // How fast the enemy moves
    public float stoppingDistance = 1.5f;  // How close to the player they stop

    private Transform player;
    private EnemyHealth health;

    internal void Start()
    {
        // Find the player once
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = GetComponent<EnemyHealth>();
    }

    internal void Update()
    {
        if (player == null) return;

        if (health != null && health.currentState == EnemyState.Dead)
            return;

        // Direction toward player
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // keep enemy on same plane (don’t move up/down)

        float distance = direction.magnitude;

        // Move only if outside stopping distance
        if (distance > stoppingDistance)
        {
            Vector3 move = direction.normalized * moveSpeed * Time.deltaTime;
            transform.position += move;

            // Rotate to face the player
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
