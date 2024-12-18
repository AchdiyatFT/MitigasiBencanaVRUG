using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VideoButtonController : MonoBehaviour
{
    public VideoPlayer videoPlayer1; // Drag video player untuk video 1
    public VideoPlayer videoPlayer2; // Drag video player untuk video 2

    public GameObject buttonBlue; // Tombol biru
    public GameObject buttonRed;  // Tombol merah

    private void Start()
    {
        // Tambahkan event listener untuk tombol biru
        buttonBlue.GetComponent<XRBaseInteractable>().activated.AddListener((args) =>
        {
            PlayVideo1();
        });

        // Tambahkan event listener untuk tombol merah
        buttonRed.GetComponent<XRBaseInteractable>().activated.AddListener((args) =>
        {
            PlayVideo2();
        });
    }

    void PlayVideo1()
    {
        // Hentikan video lain jika sedang diputar
        if (videoPlayer2.isPlaying)
            videoPlayer2.Stop();

        // Mainkan video 1
        videoPlayer1.Play();
    }

    void PlayVideo2()
    {
        // Hentikan video lain jika sedang diputar
        if (videoPlayer1.isPlaying)
            videoPlayer1.Stop();

        // Mainkan video 2
        videoPlayer2.Play();
    }
}