using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] Transform player;
    [SerializeField] Vector3 targetPosition;

    // MANAGEMENT UI
    [SerializeField] TextMeshProUGUI txt_jumlahBarangBenar;
    [SerializeField] TextMeshProUGUI txt_keterangan;
    [SerializeField] GameObject UI_Gameover;
    [SerializeField] GameObject UI_VideoPlayer;
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
            if  (time < 0)
            {
                GameOverWaktuHabis();
            }
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
    public void GameOverWaktuHabis()
    {
        txt_jumlahBarangBenar.SetText(BarangBenar + "/ 10");
        txt_keterangan.SetText("Kamu gagal dikarenakan waktu telah habis.");
        TeleportPlayer();
        GameStart = false;
    }

    public void GameOverPintuKeluar()
    {
        canTimeDecrease = false;
        GameStart = false;
        txt_jumlahBarangBenar.SetText(BarangBenar + "/ 10");

        if (BarangBenar >= 7)
        {
            txt_keterangan.SetText("Selamat!, kamu membawa barang-barang yang berguna.");
        }
        else
        {
            txt_keterangan.SetText("Kamu gagal, karena kurang membawa barang-barang yang berguna.");
        }

        TeleportPlayer();

    }

    public void TeleportPlayer()
    {
        player.position = targetPosition;
    }

    public void ShowVideoPlayer()
    {
        UI_VideoPlayer.SetActive(true);
        UI_Gameover.SetActive(false);
    }

    public void ShowGameOver()
    {
        UI_VideoPlayer.SetActive(true);
        UI_Gameover.SetActive(false);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("Scene_DalamRumah");
    }

    public void balikPosko()
    {
        SceneManager.LoadScene("Scene_Posko");
    }

}
