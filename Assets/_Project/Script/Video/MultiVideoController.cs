using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MultiVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Referensi ke Video Player
    public VideoClip video1;        // Video pertama
    public VideoClip video2;        // Video kedua

    public GameObject button1;      // Objek tombol untuk video 1
    public GameObject button2;      // Objek tombol untuk video 2
    public GameObject button3;      // Objek tombol untuk scene 1
    public GameObject button4;      // Objek tombol untuk scene 2

    private void Start()
    {
        // Nonaktifkan tombol 2, 3, dan 4 di awal
        button2.SetActive(false);
        button3.SetActive(false);
        button4.SetActive(false);

        // Tambahkan listener untuk event selesai video
        videoPlayer.loopPointReached += OnVideoFinished;
    }

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

    // Fungsi untuk mengganti ke scene 1
    public void ChangeToScene1()
    {
        SceneManager.LoadScene("Scene_DalamRumah"); // Ganti nama "Scene1" sesuai dengan nama scene Anda
    }

    // Fungsi untuk mengganti ke scene 2
    public void ChangeToScene2()
    {
        //SceneManager.LoadScene("Scene2"); // Ganti nama "Scene2" sesuai dengan nama scene Anda
    }

    // Fungsi callback ketika video selesai dimainkan
    private void OnVideoFinished(VideoPlayer vp)
    {
        if (vp.clip == video1)
        {
            // Jika video 1 selesai, aktifkan tombol 2 dan 3
            button2.SetActive(true);
            button3.SetActive(true);
        }
        else if (vp.clip == video2)
        {
            // Jika video 2 selesai, aktifkan tombol 4
            button4.SetActive(true);
        }
    }
}
