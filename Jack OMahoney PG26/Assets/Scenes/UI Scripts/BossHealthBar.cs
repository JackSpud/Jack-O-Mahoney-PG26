using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealthBar : MonoBehaviour
{
    public Slider bossSlider;
    public TextMeshProUGUI bossText;
    private EnemyHealth bossHealth;

    void Start()
    {
        gameObject.SetActive(false); // Hides the entire panel
    }

    void Update()
    {
        if (bossHealth != null)
        {
            bossSlider.value = bossHealth.currentHealth;
            bossText.text = $"{bossHealth.currentHealth} / {bossHealth.maxHealth}";
        }
    }

    public void SetBoss(EnemyHealth boss)
    {
        bossHealth = boss;

        if (bossHealth != null)
        {
            bossSlider.maxValue = bossHealth.maxHealth;
            bossSlider.value = bossHealth.currentHealth;

            gameObject.SetActive(true); // Show entire panel
        }
    }

    public void ClearBoss()
    {
        bossHealth = null;
        gameObject.SetActive(false); // Hide entire panel
    }
}