using TMPro;
using UnityEngine;

public class GameManager_DalamRumah : MonoBehaviour
{
    // MANAGEMENT WAKTU
    bool GameStart = false;
    [SerializeField] float setTime = 120f;
    [SerializeField] float time;
    bool canTimeDecrease = true;
    [SerializeField] TextMeshProUGUI textTimer;

    // MANAGEMENT SKOR
    public int BarangBenar = 0;

    // MANAGEMENT TAS
    [SerializeField] GameObject tas1;
    [SerializeField] GameObject tas2;

    // MANAGEMENT PintuKeluar
    [SerializeField] GameObject pintuKeluar;
    void Awake()
    {
        time = setTime;
        pintuKeluar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStart)
        {
            TimeDecrease();
        }

        textTimer.SetText("Waktu tersisa: " + time.ToString("F0") + "Detik");
        
    }


    // FUNGSI WAKTU
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
        GameStart = false;
    }
    

    // FUNGSI TAS

    public void DisableTas1()
    {
        tas1.SetActive(false);
        GameStart = true;
    }

    public void DisableTas2()
    {
        tas2.SetActive(false);
        GameStart = true;
    }

    // FUNGSI PINTUKELUAR

    public void EnablePintuKeluar()
    {
        pintuKeluar.SetActive(true);
    }

    // FUNGSI AKHIR PERMAINAN
    public void GameOver()
    {
        canTimeDecrease = false;
        GameStart = false;
        Debug.Log("Jumlah barang yang benar = " + BarangBenar);
    }

    public void GameOverPintuKeluar()
    {
        canTimeDecrease = false;
        GameStart = false;
        Debug.Log("Jumlah barang yang benar = " + BarangBenar);

    }
}
