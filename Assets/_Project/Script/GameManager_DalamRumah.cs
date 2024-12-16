using UnityEngine;

public class GameManager_DalamRumah : MonoBehaviour
{
    [SerializeField] float setTime = 120f;
    [SerializeField] float time;
    bool canTimeDecrease = true;

    bool GameStart = false;

    //Scoring
    public int BarangBenar = 0;

    void Awake()
    {
        time = setTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStart)
        {
            TimeDecrease();
        }
        
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
    }
}
