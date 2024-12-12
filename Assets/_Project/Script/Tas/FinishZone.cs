using UnityEngine;

public class FinishZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        tasInventory tas = other.GetComponent<tasInventory>();
        if (tas != null && tas.CheckCompletion())
        {
            Debug.Log("Simulasi selesai! Tas berhasil dibawa ke kotak.");
            CompleteSimulation();
        }
    }

    private void CompleteSimulation()
    {
        // Logika untuk menyelesaikan simulasi
        // Misalnya menampilkan UI "Selesai" atau memuat ulang scene
        Debug.Log("Menampilkan pesan sukses kepada pemain.");
    }
}
