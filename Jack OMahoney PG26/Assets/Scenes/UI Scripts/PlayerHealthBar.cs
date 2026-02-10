using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    void Start()
    {
        if (playerHealth != null && healthSlider != null)
        {
            healthSlider.maxValue = playerHealth.maxHealth;
            healthSlider.value = playerHealth.currentHealth;
            UpdateHealthText();
        }
    }

    void Update()
    {
        if (playerHealth != null && healthSlider != null)
        {
            healthSlider.value = playerHealth.currentHealth;
            UpdateHealthText();
        }
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = $"{playerHealth.currentHealth} / {playerHealth.maxHealth}";
        }
    }
}