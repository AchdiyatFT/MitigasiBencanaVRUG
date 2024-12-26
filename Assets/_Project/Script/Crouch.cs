using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class Crouch : MonoBehaviour
{
    [Header("XR Origin")]
    [SerializeField]
    private XROrigin xrOrigin;

    [Header("Character Controller")]
    [SerializeField]
    private CharacterController characterController;

    [Header("Crouch Settings")]
    [SerializeField]
    private float crouchHeight = 0.7f;  // Height when crouching
    [SerializeField]
    private float standingHeight = 1.4f; // Height when standing

    [Header("Crouch Inputs")]
    [SerializeField]
    private XRInputValueReader<float> leftCrouchInput;  // Input for left crouch button
    [SerializeField]
    private XRInputValueReader<float> rightCrouchInput; // Input for right crouch button


    private void Awake()
    {
        // Check PlayerPrefs to determine if the player is an adult or child
        //int playerAgeCategory = PlayerPrefs.GetInt("PlayerAgeCategory", 0);  // Default to 0 (adult) if not set
        if (GameManager_Posko.usia == 2) // 1 represents child in this example
        {
            // Adjust the crouch and standing heights for a child
            crouchHeight = 0.5f;
            standingHeight = 1.0f;
        }
        else
        {
            // If player is an adult, keep original values
            crouchHeight = 0.7f;
            standingHeight = 1.4f;
        }

        // Optionally, print the values for debugging
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
        // Periksa apakah salah satu input crouch aktif (dengan threshold)
        float leftValue = leftCrouchInput != null ? leftCrouchInput.ReadValue() : 0f;
        float rightValue = rightCrouchInput != null ? rightCrouchInput.ReadValue() : 0f;

        // Anggap tombol ditekan jika nilai float >= 0.5
        bool isCrouching = leftValue >= 0.5f || rightValue >= 0.5f;

        // Sesuaikan tinggi kamera dan karakter berdasarkan status crouch
        UpdateCrouchState(isCrouching);
        UpdateHeight();
    }

    private void UpdateCrouchState(bool isCrouching)
    {
        float targetHeight = isCrouching ? crouchHeight : standingHeight;

        SetCameraYOffset(targetHeight);
        UpdateCharacterControllerHeight(targetHeight);
    }

    // Function to modify the XR Origin Y position (camera height)
    private void SetCameraYOffset(float height)
    {
        if (xrOrigin != null)
        {
            Vector3 currentPosition = xrOrigin.transform.position;
            currentPosition.y = height;

            // Update the XR Origin's position to the new height
            xrOrigin.transform.position = currentPosition;
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

}
