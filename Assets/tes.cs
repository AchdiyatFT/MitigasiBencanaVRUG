using UnityEngine;
using UnityEngine.Video;

public class VRButtonVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public void Interact()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Play();
        }
    }
}
