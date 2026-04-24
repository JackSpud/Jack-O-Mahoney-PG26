using System;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    GameObject selectionPanel;
    GameObject VideoPanel;
    internal void TurnOnVideo()
    {
       VideoPanel.SetActive(true);
    }

    internal void TurnOffVideo()
    {
        VideoPanel.SetActive(false);
    }

    internal void TurnOffSelection()
    {
        selectionPanel.SetActive(false);

        print("turning off");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>(true);

        foreach (Transform child in allChildren)
        {
            if (child.name == "ChestVideoPanel")
            {
                VideoPanel = child.gameObject;
            }

            if (child.name == "BuffSelectionPanel")
            {
                selectionPanel = child.gameObject;
                print("Found selectionPanel");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
