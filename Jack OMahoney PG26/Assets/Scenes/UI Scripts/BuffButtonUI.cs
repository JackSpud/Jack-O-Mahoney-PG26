using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuffButtonUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text buffName;
    public TMP_Text description;

    public void Setup(BuffData buff)
    {
        if (icon != null)
            icon.sprite = buff.icon;

        if (buffName != null)
            buffName.text = buff.buffName;

        if (description != null)
            description.text = buff.description;
    }
}