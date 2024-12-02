using UnityEngine;

public class isKinematic : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogWarning("Rigidbody tidak ditemukan pada objek " + gameObject.name);
        }
    }

    // Metode untuk mengatur isKinematic
    public void SetKinematic(bool state)
    {
        if (rb != null)
        {
            rb.isKinematic = state;
        }
    }
}
