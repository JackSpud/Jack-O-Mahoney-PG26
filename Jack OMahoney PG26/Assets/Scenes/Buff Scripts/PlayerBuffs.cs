using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffs : MonoBehaviour
{
    public List<BuffData> activeBuffs = new List<BuffData>();

    public void AddBuff(BuffData buff)
    {
        activeBuffs.Add(buff);
        Debug.Log("Added Buff: " + buff.buffName);
    }
}