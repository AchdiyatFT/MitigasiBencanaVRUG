using UnityEngine;

public class mainMenuController : MonoBehaviour
{
    public GameObject mainMenuPanel; // Panel Main Menu

    public void HideMainMenu()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false); // Sembunyikan Main Menu
        }
    }
}
