using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class collectItem : MonoBehaviour
{
    public string itemName;
    public bool isImportant; // Menentukan apakah barang ini penting

    private bool isHeld = false; // Untuk mendeteksi apakah barang sedang dipegang
    private Collider itemCollider;

    private void Awake()
    {
        // Pastikan Collider diambil saat start
        itemCollider = GetComponent<Collider>();
        if (itemCollider == null)
        {
            Debug.LogError("Tidak ditemukan Collider pada objek ini!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Aktifkan interaksi hanya jika barang mendekati tas
        if (other.CompareTag("Bag") && !isHeld)
        {
            itemCollider.enabled = true; // Aktifkan Collider untuk mendeteksi tas
            tasInventory inventory = other.GetComponent<tasInventory>();
            if (inventory != null && inventory.AddItem(this))
            {
                Debug.Log($"{itemName} telah dimasukkan ke dalam tas.");
                // Nonaktifkan barang dari interaksi lebih lanjut (misal: sembunyikan barang)
                gameObject.SetActive(false); // Nonaktifkan barang, jangan hancurkan
            }
            else
            {
                Debug.Log("Tas penuh atau item tidak dapat dimasukkan.");
            }
        }
    }

    // Dipanggil ketika barang sedang dipegang (XR Grab Interactable Events)
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        isHeld = true;
        Debug.Log($"{itemName} sedang dipegang.");
        if (itemCollider != null)
        {
            itemCollider.enabled = false; // Nonaktifkan Collider saat barang dipegang
        }
    }

    // Dipanggil ketika barang dilepaskan
    public void OnSelectExited(SelectExitEventArgs args)
    {
        isHeld = false;
        Debug.Log($"{itemName} telah dilepaskan.");
        if (itemCollider != null)
        {
            itemCollider.enabled = true; // Aktifkan Collider kembali setelah dilepaskan
        }
    }
}