using UnityEngine;

public class ChangeColorBasedOnAge : MonoBehaviour
{
    [Header("Object Renderer")]
    [SerializeField]
    private SkinnedMeshRenderer skinnedMeshRenderer; // SkinnedMeshRenderer of the object to change color

    [Header("Color Settings")]
    [SerializeField]
    private Color adultColor = Color.blue; // Color for adult players
    [SerializeField]
    private Color childColor = Color.green; // Color for child players

    private void Start()
    {
        // Check PlayerPrefs to determine if the player is an adult or child
        int playerAgeCategory = PlayerPrefs.GetInt("PlayerAgeCategory", 0); // Default to 0 (adult) if not set

        // Change color based on the player's age category
        if (playerAgeCategory == 1) // 1 represents child in this example
        {
            // Set the object's color to the child color
            SetObjectColor(childColor);
        }
        else
        {
            // Set the object's color to the adult color
            SetObjectColor(adultColor);
        }
    }

    // Function to set the object's color
    private void SetObjectColor(Color color)
    {
        if (skinnedMeshRenderer != null)
        {
            skinnedMeshRenderer.material.color = color;
        }
        else
        {
            Debug.LogWarning("Renderer component not assigned!");
        }
    }
}
