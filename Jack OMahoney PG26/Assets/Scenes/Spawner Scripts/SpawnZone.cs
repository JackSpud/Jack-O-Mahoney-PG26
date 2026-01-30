using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public Vector3 size = new Vector3(5f, 1f, 5f);

    public Vector3 GetRandomPoint()
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-size.x / 2f, size.x / 2f),
            Random.Range(-size.y / 2f, size.y / 2f),
            Random.Range(-size.z / 2f, size.z / 2f)
        );

        return transform.position + randomOffset;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
