using UnityEngine;

public class showMainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel; // Panel Main Menu

    public void ShowMenu()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true); // Tampilkan Main Menu
        }
    }
}
