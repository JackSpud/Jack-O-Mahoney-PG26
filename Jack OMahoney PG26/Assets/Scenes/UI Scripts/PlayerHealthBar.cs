using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerStats playerStats;
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    void Start()
    {
        if (playerStats == null)
            playerStats = FindFirstObjectByType<PlayerStats>();

        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        if (playerHealth == null)
            playerHealth = FindFirstObjectByType<PlayerHealth>();

        if (playerStats == null)
            playerStats = FindFirstObjectByType<PlayerStats>();

        if (playerHealth == null || healthSlider == null)
            return;

        float maxHealth = playerStats != null
            ? playerStats.GetMaxHealth()
            : playerHealth.maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = playerHealth.currentHealth;

        if (healthText != null)
        {
            healthText.text =
                playerHealth.currentHealth.ToString("0") +
                " / " +
                maxHealth.ToString("0");
        }
    }
}