using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour
{
    [Header("Buff Setup")]
    public List<BuffData> allBuffs;
    public GameObject buffButtonPrefab;
    public Transform buffOptionsParent;
    public GameObject selectionPanel;

    [Header("Settings")]
    public int defaultChoices = 3;

    public void ShowBuffChoices(int amount = -1)
    {
        if (amount <= 0)
            amount = defaultChoices;

        // Show panel
        selectionPanel.SetActive(true);

        // Pause game
        Time.timeScale = 0f;

        // Unlock cursor so we can click
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Clear old buttons
        foreach (Transform child in buffOptionsParent)
            Destroy(child.gameObject);

        // Create temporary copy so we don't repeat buffs
        List<BuffData> tempList = new List<BuffData>(allBuffs);

        for (int i = 0; i < amount; i++)
        {
            if (tempList.Count == 0)
                break;

            int randomIndex = Random.Range(0, tempList.Count);
            BuffData chosen = tempList[randomIndex];
            tempList.RemoveAt(randomIndex);

            // Spawn button
            GameObject button = Instantiate(buffButtonPrefab);
            button.transform.SetParent(buffOptionsParent, false);

            // Optional: Set icon if your prefab has an Image
            Image iconImage = button.GetComponent<Image>();
            if (iconImage != null && chosen.icon != null)
                iconImage.sprite = chosen.icon;

            // Add click listener
            Button btn = button.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                ApplyBuff(chosen);
            });
        }
    }

    void ApplyBuff(BuffData buff)
    {
        PlayerBuffs player = FindFirstObjectByType<PlayerBuffs>();
        if (player != null)
            player.AddBuff(buff);

        BuffUIManager ui = FindFirstObjectByType<BuffUIManager>();
        if (ui != null && player != null)
        {
            int slotIndex = player.activeBuffs.Count - 1;
            ui.SetBuff(slotIndex, buff.icon);
        }

        selectionPanel.SetActive(false);

      
        Time.timeScale = 1f;

        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Temporary test key
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ShowBuffChoices();
        }
    }
}