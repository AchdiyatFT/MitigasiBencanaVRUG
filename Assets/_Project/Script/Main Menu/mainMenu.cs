using UnityEngine;

public class FollowHead : MonoBehaviour
{
    public Transform head; // Referensi ke posisi kepala (kamera VR)
    public float distance = 2f; // Jarak menu dari kepala pengguna

    void Update()
    {
        if (head != null)
        {
            // Tempatkan menu di depan kepala
            transform.position = head.position + head.forward * distance;

            // Rotasi menu untuk menghadap pengguna
            transform.rotation = Quaternion.LookRotation(transform.position - head.position);
        }
    }
}