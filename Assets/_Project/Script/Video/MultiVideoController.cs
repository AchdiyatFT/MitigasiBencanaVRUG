using UnityEngine;
using UnityEngine.Video;

public class MultiVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Referensi ke Video Player
    public VideoClip video1;        // Video pertama
    public VideoClip video2;        // Video kedua

    // Fungsi untuk memutar video pertama
    public void PlayVideo1()
    {
        if (videoPlayer.clip != video1) // Cek apakah video 1 belum diputar
        {
            videoPlayer.clip = video1; // Ganti clip ke video 1
            videoPlayer.Play();        // Mulai pemutaran
        }
    }

    // Fungsi untuk memutar video kedua
    public void PlayVideo2()
    {
        if (videoPlayer.clip != video2) // Cek apakah video 2 belum diputar
        {
            videoPlayer.clip = video2; // Ganti clip ke video 2
            videoPlayer.Play();        // Mulai pemutaran
        }
    }
}
