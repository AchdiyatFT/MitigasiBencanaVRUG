using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class ButtonVR : MonoBehaviour
{
    public GameObject button; // Referensi tombol press
    public UnityEvent onPress; // Event untuk tombol ditekan
    public UnityEvent onRelease; // Event untuk tombol dilepas
    public VideoPlayer videoPlayer; // Referensi VideoPlayer
    GameObject presser; // Objek yang menekan tombol
    AudioSource sound; // Suara tombol
    bool isPressed; // Kondisi tombol

    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed) // Validasi tag controller
        {
            button.transform.localPosition = new Vector3(0, 0.003f, 0); // Posisi tombol tertekan
            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser) // Periksa apakah yang keluar adalah presser
        {
            button.transform.localPosition = new Vector3(0, 0.015f, 0); // Kembalikan posisi tombol
            onRelease.Invoke();
            isPressed = false;
            presser = null; // Reset presser
        }
    }

    public void SpawnSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.localPosition = new Vector3(0, 1, 2);
        sphere.AddComponent<Rigidbody>();
    }
}
