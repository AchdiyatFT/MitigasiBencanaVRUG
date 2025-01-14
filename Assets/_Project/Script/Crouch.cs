using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class Crouch : MonoBehaviour
{
    [Header("XR Origin")]
    [SerializeField]
    private XROrigin xrOrigin;
    [SerializeField] private Transform cameraOffset;

    [Header("Character Controller")]
    [SerializeField]
    private CharacterController characterController;
    private Vector3 initialPosition;

    [Header("Crouch Settings")]
    private float crouchHeight;  // Height when crouching
    private float standingHeight; // Height when standing
    [SerializeField] float crouchValueDewasa = 0f;
    [SerializeField] float standingValueDewasa = 0f;
    [SerializeField] float crouchValueAnak = 0f;
    [SerializeField] float standingValueAnak = 0f;

    [Header("Crouch Inputs")]
    [SerializeField]
    private XRInputValueReader<float> leftCrouchInput;  // Input for left crouch button
    [SerializeField]
    private XRInputValueReader<float> rightCrouchInput; // Input for right crouch button


    private void Awake()
    {
        // Periksa kategori usia untuk menentukan tinggi crouch dan standing
        if (GameManager_Posko.usia == 2) // 1 represents child
        {
            crouchHeight = crouchValueAnak;
            standingHeight = standingValueAnak;
        }
        else
        {
            crouchHeight = crouchValueDewasa;
            standingHeight = standingValueDewasa;
        }

        Debug.Log($"Crouch Height: {crouchHeight}, Standing Height: {standingHeight}");
    }

    private void OnEnable()
    {
        // Enable input readers for crouch actions
        leftCrouchInput?.EnableDirectActionIfModeUsed();
        rightCrouchInput?.EnableDirectActionIfModeUsed();
    }

    private void OnDisable()
    {
        // Disable input readers when not needed
        leftCrouchInput?.DisableDirectActionIfModeUsed();
        rightCrouchInput?.DisableDirectActionIfModeUsed();
    }

    private void Update()
    {
        // Periksa apakah input crouch aktif
        float leftValue = leftCrouchInput != null ? leftCrouchInput.ReadValue() : 0f;
        float rightValue = rightCrouchInput != null ? rightCrouchInput.ReadValue() : 0f;

        // Anggap tombol crouch ditekan jika nilai float >= 0.5
        bool isCrouching = leftValue >= 0.5f || rightValue >= 0.5f;

        // Atur posisi Camera Offset berdasarkan status crouch
        UpdateCameraOffset(isCrouching);
    }

    private void UpdateCrouchState(bool isCrouching)
    {
        float targetHeight = isCrouching ? crouchHeight : standingHeight;

       //pdateCharacterControllerHeight(targetHeight);
        AdjustCameraOffset(targetHeight);
    }

    // Function to modify the XR Origin Y position (camera height)
    private void SetCameraYOffset(float height)
    {
        if (xrOrigin != null)
        {
            // Posisi baru dihitung dari posisi awal, ditambah tinggi baru
            Vector3 targetPosition = initialPosition;
            targetPosition.y = height;

            // Update posisi XR Origin ke tinggi baru
            xrOrigin.transform.position = targetPosition;
        }
    }

    // Function to update the Character Controller's height
    private void UpdateCharacterControllerHeight(float height)
    {
        if (characterController != null)
        {
            // Update the height of the CharacterController
            characterController.height = height;

            // Adjust center of CharacterController to match the new height
            Vector3 center = characterController.center;
            center.y = height / 2;
            characterController.center = center;
        }
    }

    private void AdjustCameraOffset(float targetHeight)
    {
        if (cameraOffset != null && characterController != null)
        {
            // Set the Y position of the Camera Offset to align with the Character Controller
            Vector3 offsetPosition = cameraOffset.localPosition;
            offsetPosition.y = characterController.center.y; // Align with the center of the Character Controller
            cameraOffset.localPosition = offsetPosition;
        }
    }

    private void UpdateCameraOffset(bool isCrouching)
    {
        if (cameraOffset != null)
        {
            // Tentukan tinggi target berdasarkan status crouch
            float targetHeight = isCrouching ? crouchHeight : standingHeight;

            // Perbarui posisi Y dari Camera Offset
            Vector3 offsetPosition = cameraOffset.localPosition;
            offsetPosition.y = targetHeight; // Hanya sesuaikan posisi vertikal
            cameraOffset.localPosition = offsetPosition;
        }
    }

}
