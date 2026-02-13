using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public float floatSpeed = 2f;
    public float lifeTime = 1f;

    private TextMeshProUGUI textMesh;
    private Color startColor;

    void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        startColor = textMesh.color;
    }

    public void SetDamage(float damage)
    {
        textMesh.text = damage.ToString();
    }

    void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        lifeTime -= Time.deltaTime;

        float alpha = Mathf.Clamp01(lifeTime);
        textMesh.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        // Make it always face camera
        if (Camera.main != null)
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}