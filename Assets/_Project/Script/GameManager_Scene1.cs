using UnityEngine;

public class GameManager_Scene1 : MonoBehaviour
{
    [SerializeField] float setTime = 120f;
    [SerializeField] float time;
    bool canTimeDecrease = true;

    //Scoring
    public int BarangBenar = 0;

    void Awake()
    {
        time = setTime;
    }

    // Update is called once per frame
    void Update()
    {
        TimeDecrease();
    }

    void TimeDecrease()
    {
        if (canTimeDecrease && time >= 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            GameOver();
        }

    }
    public void ResetTime()
    {
        time = setTime;
    }

    public void GameOver()
    {
        canTimeDecrease = false;
        Debug.Log("Jumlah barang yang benar = " + BarangBenar);
        Debug.Log("Waktu yang tersisa = " + time);
    }
}
