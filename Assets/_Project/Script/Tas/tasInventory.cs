using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class tasInventory : MonoBehaviour
{
    private List<collectItem> items = new List<collectItem>();
    public int maxCapacity = 10;
    public int requiredImportantItems = 3;
    private bool isReadyToBeCarried = false;

    public Collider bagCollider;
    public XRGrabInteractable grabInteractable;
    public XRSocketInteractor socketInteractor;

    [Header("Inventory Data")]
    public InventoryData inventoryData;

    [Header("Bag Appearance")]
    public GameObject defaultBagModel2;  // Default bag model (Model 2)
    public GameObject carriedBagModel1; // Carried bag model (Model 1)
    public GameObject fullBagModel3;    // Full bag model (Model 3)
    public GameObject socketedBagModel4;

    [SerializeField] GameManager_DalamRumah gameManager;

    private enum BagState
    {
        Default,    // Model saat tas idle
        Carried,    // Model saat tas dibawa
        Socketed,   // Model saat tas di socket
        Full        // Model saat tas penuh
    }

    private BagState currentState = BagState.Default; // State awal tas

    private void Start()
    {
        bagCollider = GetComponent<Collider>();
        if (bagCollider == null)
        {
            Debug.LogError("Tidak ada Collider pada tas!");
        }

        if (inventoryData != null)
        {
            inventoryData.ClearData();
        }

        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.AddListener(OnConnectedToSocket);
            socketInteractor.selectExited.AddListener(OnDisconnectedFromSocket);
        }

        UpdateBagAppearance(); // Initialize bag model
    }

    public bool AddItem(collectItem item)
    {
        if (items.Count < maxCapacity)
        {
            items.Add(item);
            Debug.Log($"Barang {item.itemName} ditambahkan ke tas.");

            if (inventoryData != null)
            {
                inventoryData.AddItem(item.itemName, item.isImportant);
            }

            CheckIfReadyToCarry();
            UpdateBagAppearance(); // Update appearance
            return true;
        }
        Debug.Log("Tas penuh, tidak bisa menambahkan barang.");
        return false;
    }

    private void CheckIfReadyToCarry()
    {
        if (CheckCompletion() && !isReadyToBeCarried)
        {
            isReadyToBeCarried = true;

            // Pastikan tas tetap dapat diambil meskipun penuh
            if (grabInteractable != null)
            {
                grabInteractable.enabled = true;
            }
        }
    }

    private void OnConnectedToSocket(SelectEnterEventArgs args)
    {
        Debug.Log("OnConnectedToSocket dipanggil.");
        if (bagCollider != null)
        {
            bagCollider.isTrigger = true;
            Debug.Log("Tas terhubung ke socket, collider diatur menjadi trigger.");
        }

        SetBagState(BagState.Socketed); // Ubah ke mode Socketed
    }

    private void OnDisconnectedFromSocket(SelectExitEventArgs args)
    {
        Debug.Log("OnDisconnectedFromSocket dipanggil.");
        if (bagCollider != null)
        {
            bagCollider.isTrigger = false; // Nonaktifkan isTrigger
            Debug.Log("Tas dilepas dari socket, collider diatur menjadi non-trigger.");
        }

        if (CheckCompletion())
        {
            SetBagState(BagState.Full);
        }
        else
        {
            SetBagState(BagState.Default); // Kembali ke mode Default
        }

        // Pastikan tas tetap dapat diambil
        if (grabInteractable != null)
        {
            grabInteractable.enabled = true;
        }
    }

    public bool CheckCompletion()
    {
        return items.Count >= maxCapacity;
    }

    private void UpdateBagAppearance()
    {
        if (defaultBagModel2 == null || carriedBagModel1 == null || fullBagModel3 == null || socketedBagModel4 == null)
        {
            Debug.LogError("Salah satu model tas (defaultBagModel2, carriedBagModel1, atau fullBagModel3) belum diatur!");
            return;
        }

        SetActiveBagModel(null);

        switch (currentState)
        {
            case BagState.Default:
                SetActiveBagModel(defaultBagModel2);
                Debug.Log("Tampilan tas: Default.");
                break;

            case BagState.Carried:
                SetActiveBagModel(carriedBagModel1);
                Debug.Log("Tampilan tas: Carried.");
                break;

            case BagState.Socketed:
                SetActiveBagModel(socketedBagModel4);
                Debug.Log("Tampilan tas: Socketed.");
                break;

            case BagState.Full:
                SetActiveBagModel(fullBagModel3);
                Debug.Log("Tampilan tas: Full.");
                break;
        }
    }

    private void SetBagState(BagState newState)
    {
        Debug.Log($"SetBagState dipanggil: {newState}");

        if (newState == BagState.Socketed)
        {
            currentState = BagState.Socketed;
            Debug.Log("State diubah ke Socketed.");
        }
        else if (items.Count >= maxCapacity)
        {
            currentState = BagState.Full;
            Debug.Log("Tas penuh! State berubah menjadi Full.");

            if (bagCollider != null)
            {
                bagCollider.isTrigger = false;
                Debug.Log("Collider diatur menjadi non-trigger karena tas penuh.");
            }
        }
        else
        {
            currentState = newState;
        }

        UpdateBagAppearance(); // Perbarui tampilan sesuai state baru
    }

    private void SetActiveBagModel(GameObject activeModel)
    {
        if (defaultBagModel2 != null) defaultBagModel2.SetActive(false);
        if (carriedBagModel1 != null) carriedBagModel1.SetActive(false);
        if (fullBagModel3 != null) fullBagModel3.SetActive(false);
        if (socketedBagModel4 != null) socketedBagModel4.SetActive(false);

        if (activeModel != null)
        {
            activeModel.SetActive(true);
        }
    }

    void Update()
    {
        Debug.Log($"Current State: {currentState}");

        if (items.Count >= maxCapacity && currentState != BagState.Full)
        {
            SetBagState(BagState.Full);
        }
    }
}
