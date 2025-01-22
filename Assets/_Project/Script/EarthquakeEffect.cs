using System.Collections;
using UnityEngine;

public class EarthquakeEffect : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    //public float duration = 1.0f; // Durasi gempa
    //public float magnitude = 0.1f; // Intensitas gempa

    private Vector3 originalPosition;

    private void Start()
    {
        // Simpan posisi awal XR Rig atau objek induk kamera
        originalPosition = transform.localPosition;
        TriggerEarthquake();
    }

    public void TriggerEarthquake()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        //float elapsedTime = 0f;

        while (gameManager.Camera_Earthquake_shake >= 0)
        {
            // Buat pergeseran posisi acak
            float offsetX = Random.Range(-gameManager.Camera_Earthquake_shake, gameManager.Camera_Earthquake_shake);
            float offsetZ = Random.Range(-gameManager.Camera_Earthquake_shake, gameManager.Camera_Earthquake_shake); // Biasanya lebih baik hindari Y untuk VR

            transform.localPosition = new Vector3(
                originalPosition.x + offsetX,
                originalPosition.y, // Jangan ganggu tinggi (Y-axis) untuk menghindari ketidaknyamanan
                originalPosition.z + offsetZ
            );

            yield return null; // Tunggu frame berikutnya
        }

        // Kembalikan posisi XR Rig ke posisi awal
        transform.localPosition = originalPosition;
    }
}
