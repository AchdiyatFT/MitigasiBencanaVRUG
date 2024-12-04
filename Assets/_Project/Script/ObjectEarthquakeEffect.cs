using UnityEngine;

public class ObjectEarthquakeEffect : MonoBehaviour
{
    private GameObject targetObject; // Objek yang akan digoyangkan
    [SerializeField] GameManager gameManager;
    [SerializeField] float detectionRange = 0.3f;
    //public float Earthquake_shake = 0f; // Intensitas guncangan
    //public float shakeAmount = 1f; // Seberapa jauh objek bergoyang
    //public float shakeSpeed = 10f; // Kecepatan guncangan
    public LayerMask groundLayer; // Layer yang dianggap sebagai lantai

    private Vector3 currentOffset = Vector3.zero; // Offset untuk guncangan

    void Start()
    {
        targetObject = GetComponent<GameObject>();


        if (targetObject == null)
        {
            targetObject = this.gameObject; // Default ke objek ini jika tidak diatur
        }
    }

    void Update()
    {
        if (IsGrounded())
        {
            //Earthquake_shake = 1f; // Atur intensitas guncangan saat menyentuh lantai
            ShakeObject();
        }
        else
        {
            //Earthquake_shake = 0f; // Hentikan guncangan saat tidak menyentuh lantai
        }
    }

    void ShakeObject()
    {
        // Buat offset acak berdasarkan intensitas Earthquake_shake
        currentOffset = new Vector3(
            Random.Range(-1f, 1f) * gameManager.Obj_Earthquake_shake * gameManager.Obj_shake_speed,
            Random.Range(-1f, 1f) * gameManager.Obj_Earthquake_shake * gameManager.Obj_shake_speed,
            0f // Anda bisa menggoyangkan pada sumbu Z jika diinginkan
        );

        // Tambahkan offset ke posisi objek untuk menggoyangkan
        targetObject.transform.position += currentOffset * Time.deltaTime * gameManager.Obj_shake_speed;
    }

    bool IsGrounded()
    {
        Debug.DrawRay(targetObject.transform.position, Vector3.down * detectionRange, Color.red);
        // Periksa apakah objek menyentuh lantai menggunakan raycast
        return Physics.Raycast(targetObject.transform.position, Vector3.down, detectionRange, groundLayer);
        
    }
}
