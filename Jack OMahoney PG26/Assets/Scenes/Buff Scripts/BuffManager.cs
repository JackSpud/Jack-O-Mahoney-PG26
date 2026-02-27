using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour
{
    public List<BuffData> allBuffs;
    public GameObject buffButtonPrefab;
    public Transform buffOptionsParent;
    public GameObject selectionPanel;

    public void ShowBuffChoices(int amount = 3)
    {
        selectionPanel.SetActive(true);

        foreach (Transform child in buffOptionsParent)
            Destroy(child.gameObject);

        List<BuffData> tempList = new List<BuffData>(allBuffs);

        for (int i = 0; i < amount; i++)
        {
            if (tempList.Count == 0) break;

            int randomIndex = Random.Range(0, tempList.Count);
            BuffData chosen = tempList[randomIndex];
            tempList.RemoveAt(randomIndex);

            GameObject button = Instantiate(buffButtonPrefab);

            RectTransform rect = button.GetComponent<RectTransform>();

            rect.SetParent(buffOptionsParent, false);
            rect.localPosition = Vector3.zero;
            rect.localScale = Vector3.one;

            Button btn = button.GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                ApplyBuff(chosen);
            });
        }
    }

    void ApplyBuff(BuffData buff)
    {
        PlayerBuffs player = FindFirstObjectByType<PlayerBuffs>();
        player.AddBuff(buff);

        BuffUIManager ui = FindFirstObjectByType<BuffUIManager>();

        if (ui != null)
        {
            int slotIndex = player.activeBuffs.Count - 1;
            ui.SetBuff(slotIndex, buff.icon);
        }

        selectionPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ShowBuffChoices();
        }
    }
}