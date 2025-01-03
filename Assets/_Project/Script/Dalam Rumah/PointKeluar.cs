using UnityEngine;

public class PointKeluar : MonoBehaviour
{
    private bool playerInZone = false;
    private bool bagInZone = false;
    [SerializeField] GameManager_DalamRumah gameManager;

    private void OnTriggerEnter(Collider other)
    {
        // Cek jika objek yang masuk adalah pemain
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
        }
        // Cek jika objek yang masuk adalah tas
        else if (other.CompareTag("Bag"))
        {
            bagInZone = true;
        }

        // Aktifkan fungsi jika kedua kondisi terpenuhi
        if (playerInZone && bagInZone)
        {
            ActivateTrigger();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Cek jika pemain keluar
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
        }
        // Cek jika tas keluar
        else if (other.CompareTag("Bag"))
        {
            bagInZone = false;
        }
    }

    private void ActivateTrigger()
    {
        gameManager.GameOverPintuKeluar();  
        // Tambahkan logika lainnya di sini
    }
}

