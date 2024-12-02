using UnityEngine;


public class TasGrabHandler : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable; // Komponen untuk interaksi grab
    private tasInventory inventory; // Referensi ke skrip tasInventory

    private void Awake()
    {
        // Ambil komponen XRGrabInteractable
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("Tidak ada XRGrabInteractable pada tas! Tambahkan komponen ini di Unity Editor.");
        }

        // Ambil referensi ke tasInventory
        inventory = GetComponent<tasInventory>();
        if (inventory == null)
        {
            Debug.LogError("Tidak ada tasInventory pada tas! Tambahkan skrip ini ke tas.");
        }

        // Nonaktifkan interaksi grab di awal
        if (grabInteractable != null)
        {
            grabInteractable.enabled = false;
        }
    }

    private void Update()
    {
        // Cek apakah tas siap untuk dibawa
        if (inventory != null && inventory.CheckCompletion())
        {
            EnableGrab(); // Aktifkan grab jika tas penuh
        }
    }

    private void EnableGrab()
    {
        if (grabInteractable != null && !grabInteractable.enabled)
        {
            grabInteractable.enabled = true; // Aktifkan grab interaksi
            Debug.Log("Tas sekarang dapat diambil oleh pemain.");
        }
    }
}
