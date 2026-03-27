using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    public GameObject promptText;
    public BuffManager buffManager;

    PanelManager videoManager;

    bool playerNearby;

    void Awake()
    {
        videoManager = FindFirstObjectByType<PanelManager>();
        if (buffManager == null)
        {
            buffManager = FindFirstObjectByType<BuffManager>();
        }
    }

    void Start()
    {
        promptText.SetActive(false);
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.F))
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        promptText.SetActive(false);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        videoManager.TurnOnVideo();

        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            promptText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            promptText.SetActive(false);
        }
    }
}