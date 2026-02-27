using UnityEngine;

[CreateAssetMenu(fileName = "NewBuff", menuName = "Buff System/Buff")]
public class BuffData : ScriptableObject
{
    public string buffName;
    public Sprite icon;
    [TextArea] public string description;

    public float bonusDamage;
    public float attackSpeedMultiplier = 1f;
    public float movementSpeedMultiplier = 1f;
    public float HealthIncrease;
}