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
        bossSlider.gameObject.SetActive(false); // hide by default
        bossText.gameObject.SetActive(false);
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
            bossSlider.gameObject.SetActive(true);
            bossText.gameObject.SetActive(true);
        }
    }

    public void ClearBoss()
    {
        bossHealth = null;
        bossSlider.gameObject.SetActive(false);
        bossText.gameObject.SetActive(false);
    }
}
