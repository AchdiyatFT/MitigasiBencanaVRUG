using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Drag & drop your VideoPlayer object here.
    public Transform visualButton; // Drag & drop your "Visual" button here.
    public float pressDepth = 0.02f; // Depth the button moves when pressed.

    private Vector3 initialPosition; // To store the initial position of the button.
    private bool isPressed = false;

    void Start()
    {
        if (visualButton != null)
        {
            initialPosition = visualButton.localPosition;
        }
    }

    public void OnButtonPressed()
    {
        if (!isPressed && videoPlayer != null)
        {
            isPressed = true;
            // Move button down.
            if (visualButton != null)
            {
                visualButton.localPosition = initialPosition - new Vector3(0, pressDepth, 0);
            }

            // Toggle play/pause for the video.
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
            }
            else
            {
                videoPlayer.Play();
            }

            // Reset the button state after a short delay.
            Invoke(nameof(ResetButton), 0.2f);
        }
    }

    private void ResetButton()
    {
        isPressed = false;
        if (visualButton != null)
        {
            visualButton.localPosition = initialPosition;
        }
    }
}
