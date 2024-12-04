using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float Camera_Earthquake_shake = 0f;
    public float Camera_Max_Earthquake_shake = 0.1f;
    public float Obj_Earthquake_shake = 0f;
    public float Obj_Max_Earthquake_shake = 0.3f;
    public float Accelerate_shake = 0.01f;
    public float Decelerate_shake = 0.01f;
    public float Camera_shake_speed = 0.1f;
    public float Obj_shake_speed = 0.3f;
    public bool Earthquake = false;
    public float Earthquake_duration = 5f;

    void Start()
    {
        
        Earthquake = false;
    }

    void Update()
    {
        Earthquake_start();
    }

    public void Earthquake_start()
    {
        if (Earthquake)
        {
            // Tingkatkan nilai Earthquake_shake
            Camera_Earthquake_shake = Mathf.Lerp(Camera_Earthquake_shake, Camera_Max_Earthquake_shake, Accelerate_shake * Time.deltaTime);
            Obj_Earthquake_shake = Mathf.Lerp(Obj_Earthquake_shake, Obj_Max_Earthquake_shake, Accelerate_shake * Time.deltaTime);
            Earthquake_duration -= Time.deltaTime;

            if (Earthquake_duration <= 0f)
            {
                Earthquake = false;
            }
        }
        else
        {
            // Kurangi nilai Earthquake_shake menuju 0
            Camera_Earthquake_shake = Mathf.MoveTowards(Camera_Earthquake_shake, 0f, Decelerate_shake * Time.deltaTime);
            Obj_Earthquake_shake = Mathf.MoveTowards(Obj_Earthquake_shake, 0f, Decelerate_shake * Time.deltaTime);
            // Opsional: Paksa nilai ke 0 jika sudah sangat dekat
            if (Mathf.Abs(Camera_Earthquake_shake) < 0.0001f)
            {
                Camera_Earthquake_shake = 0f;
                Obj_Earthquake_shake = 0f;
            }
        }
    }
}
