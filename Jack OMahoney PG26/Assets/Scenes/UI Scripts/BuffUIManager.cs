using UnityEngine;
using UnityEngine.UI;

public class BuffUIManager : MonoBehaviour
{
    public Image[] buffSlots; // assign in inspector
    public Sprite emptySprite; // placeholder when slot is empty

    // Call this to set a buff icon
    public void SetBuff(int slotIndex, Sprite buffIcon)
    {
        if (slotIndex < 0 || slotIndex >= buffSlots.Length) return;
        buffSlots[slotIndex].sprite = buffIcon;
        buffSlots[slotIndex].enabled = true;
    }

    // Call this to remove a buff for testing purposes
    public void ClearBuff(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= buffSlots.Length) return;
        buffSlots[slotIndex].sprite = emptySprite;
        buffSlots[slotIndex].enabled = false;
    }
}
