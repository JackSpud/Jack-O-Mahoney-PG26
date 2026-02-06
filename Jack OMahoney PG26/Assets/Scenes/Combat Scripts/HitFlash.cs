using System.Collections;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    public Color flashColor = Color.white;
    public float flashDuration = 0.1f;

    private Renderer rend;
    private Color originalColor;

    void Awake()
    {
        rend = GetComponentInChildren<Renderer>();
        originalColor = rend.material.color;
    }

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        rend.material.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        rend.material.color = originalColor;
    }
}
