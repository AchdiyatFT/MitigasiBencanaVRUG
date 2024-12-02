using System.Collections;
using UnityEngine;

public class efekGempa : MonoBehaviour
{
    public float shakeDuration = 2f; // Total durasi gempa
    public float magnitude = 0.5f; // Magnitudo gempa maksimum
    private float elapsedTime = 0f;
    private Vector3 originalPosition;

    // Referensi ke skrip pengelola Rigidbody
    public isKinematic rbManager;

    public float repeatInterval = 10f; // Interval pengulangan gempa dalam detik

    void Start()
    {
        originalPosition = transform.position;

        // Memastikan isKinematic terhubung
        if (rbManager == null)
        {
            rbManager = GetComponent<isKinematic>();
        }

        // Mulai pengulangan efek gempa
        StartCoroutine(RepeatEarthquake());
    }

    private IEnumerator RepeatEarthquake()
    {
        while (true)
        {
            yield return StartCoroutine(Shake()); // Mulai gempa
            yield return new WaitForSeconds(repeatInterval); // Tunggu sebelum gempa berikutnya
        }
    }

    private IEnumerator Shake()
    {
        elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            // Normalisasi waktu [0, 1]
            float normalizedTime = elapsedTime / shakeDuration;

            // Kurva bell menggunakan fungsi sin
            float shakeFactor = Mathf.Sin(normalizedTime * Mathf.PI);

            // Offset acak untuk gempa
            float x = Random.Range(-magnitude, magnitude) * shakeFactor;
            float y = Random.Range(-magnitude, magnitude) * shakeFactor;

            // Terapkan posisi getaran ke objek utama
            transform.position = originalPosition + new Vector3(x, y, 0);

            // Nonaktifkan isKinematic saat gempa berlangsung
            rbManager?.SetKinematic(false);

            // Update waktu berlalu
            elapsedTime += Time.deltaTime;

            yield return null; // Tunggu frame berikutnya
        }

        // Reset posisi ke semula setelah gempa selesai
        transform.position = originalPosition;

        // Aktifkan kembali isKinematic
        rbManager?.SetKinematic(true);
    }
}
