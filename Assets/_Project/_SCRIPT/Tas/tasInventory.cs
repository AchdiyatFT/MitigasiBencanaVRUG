using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class tasInventory : MonoBehaviour
{
    private List<collectItem> items = new List<collectItem>();
    public int maxCapacity = 10;
    public int requiredImportantItems = 3; // Jumlah item penting yang diperlukan
    private bool isReadyToBeCarried = false; // Apakah tas sudah bisa dibawa

    public Collider bagCollider; // Collider tas
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable; // Komponen untuk interaksi VR
    public XRSocketInteractor socketInteractor; // XR Socket Interactor yang berhubungan dengan tas

    [Header("Inventory Data")]
    public InventoryData inventoryData; // Referensi ke ScriptableObject untuk penyimpanan

    private void Start()
    {
        // Ambil collider tas
        bagCollider = GetComponent<Collider>();
        if (bagCollider == null)
        {
            Debug.LogError("Tidak ada Collider pada tas!");
        }

        // Nonaktifkan interaksi grab di awal permainan
        if (grabInteractable != null)
        {
            grabInteractable.interactionLayers = InteractionLayerMask.GetMask("Disabled"); // Blokir grab
        }

        // Reset data inventory saat mulai permainan
        if (inventoryData != null)
        {
            inventoryData.ClearData();
        }

        // Pastikan socket interactor tidak null
        if (socketInteractor != null)
        {
            // Tambahkan listener ke event socket interactor
            socketInteractor.selectEntered.AddListener(OnConnectedToSocket); // Tas terhubung ke socket
            socketInteractor.selectExited.AddListener(OnDisconnectedFromSocket); // Tas dilepas dari socket
        }
    }

    public bool AddItem(collectItem item)
    {
        if (items.Count < maxCapacity)
        {
            items.Add(item); // Tambahkan data barang ke dalam daftar
            Debug.Log($"Barang {item.itemName} ditambahkan ke tas.");

            // Simpan data barang ke ScriptableObject
            if (inventoryData != null)
            {
                inventoryData.AddItem(item.itemName, item.isImportant);
            }

            CheckIfReadyToCarry(); // Cek jika tas sudah bisa dibawa
            return true;
        }
        Debug.Log("Tas penuh, tidak bisa menambahkan barang.");
        return false; // Tas penuh
    }

    private void CheckIfReadyToCarry()
    {
        if (CheckCompletion() && !isReadyToBeCarried)
        {
            isReadyToBeCarried = true; // Tas siap dibawa
            EnableCarrying(); // Aktifkan interaksi VR
        }
    }

    private void EnableCarrying()
    {
        if (grabInteractable != null)
        {
            grabInteractable.interactionLayers = InteractionLayerMask.GetMask("Default"); // Aktifkan grab
            Debug.Log("Tas siap untuk dibawa oleh pemain menggunakan controller.");
        }
    }

    public bool CheckCompletion()
    {
        int importantCount = 0;
        foreach (var item in items)
        {
            if (item.isImportant)
            {
                importantCount++;
            }
        }
        return items.Count >= maxCapacity || importantCount >= requiredImportantItems;
    }

    private void OnConnectedToSocket(SelectEnterEventArgs args)
    {
        if (bagCollider != null)
        {
            bagCollider.isTrigger = true; // Aktifkan trigger saat tas terhubung ke socket
            Debug.Log("Tas terhubung ke socket, collider diatur menjadi trigger.");
        }
    }

    private void OnDisconnectedFromSocket(SelectExitEventArgs args)
    {
        if (bagCollider != null)
        {
            bagCollider.isTrigger = false; // Nonaktifkan trigger saat tas dilepas dari socket
            Debug.Log("Tas dilepas dari socket, collider dinonaktifkan sebagai trigger.");
        }

        if (CheckIfBagIsFull())
        {
            bagCollider.isTrigger = false; // Nonaktifkan trigger jika tas penuh
            Debug.Log("Tas penuh, collider dinonaktifkan sebagai trigger.");
        }
    }

    private bool CheckIfBagIsFull()
    {
        return items.Count >= maxCapacity;
    }

    // Dipanggil saat tas diambil oleh pemain
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("Tas diambil oleh pemain.");
    }

    // Dipanggil saat tas dilepaskan oleh pemain
    private void OnReleased(SelectExitEventArgs args)
    {
        Debug.Log("Tas dilepaskan oleh pemain.");
    }
}
