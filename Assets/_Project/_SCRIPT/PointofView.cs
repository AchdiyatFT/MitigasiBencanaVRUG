using System.Collections;
using UnityEngine;

public class PointofView : MonoBehaviour
{
    public float duration = 1.0f; // Durasi gempa
    public float magnitude = 0.1f; // Intensitas gempa maksimum

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
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Hitung waktu normalisasi [0, 1]
            float normalizedTime = elapsedTime / duration;

            // Terapkan kurva bell untuk intensitas getaran
            float intensityFactor = Mathf.Sin(normalizedTime * Mathf.PI);

            // Buat pergeseran posisi berdasarkan intensitas
            float offsetX = Random.Range(-magnitude, magnitude) * intensityFactor;
            float offsetZ = Random.Range(-magnitude, magnitude) * intensityFactor;

            transform.localPosition = new Vector3(
                originalPosition.x + offsetX,
                originalPosition.y, // Jangan ganggu tinggi (Y-axis)
                originalPosition.z + offsetZ
            );

            elapsedTime += Time.deltaTime;

            yield return null; // Tunggu frame berikutnya
        }

        // Kembalikan posisi XR Rig ke posisi awal
        transform.localPosition = originalPosition;
    }
}
