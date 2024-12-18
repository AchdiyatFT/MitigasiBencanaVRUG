using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_Posko : MonoBehaviour
{

    public static int usia = 0;
    [SerializeField] Material materialDewasa;
    [SerializeField] Material materialAnak2;
    [SerializeField] GameObject[] objekUntukDiubah;

    private void Start()
    {
        GantiMaterialTangan();
    }
    // SCENE MAIN MENU
    public void PilihDewasa()
    {
        usia = 1;
        LoadScene_posko();
        GantiMaterialTangan();
    }

    public void PilihAnak2()
    {
        usia = 2;
        LoadScene_posko();
        GantiMaterialTangan();
    }

    void LoadScene_posko()
    {
        SceneManager.LoadScene("Scene_Posko");
    }

    void GantiMaterialTangan()
    {
        foreach (GameObject obj in objekUntukDiubah)
        {
            if (obj.TryGetComponent<Renderer>(out Renderer renderer))
            {
                switch (usia) {
                    case 0:
                        Debug.Log("Usia belum dipilih");
                        break;
                    case 1:
                        renderer.material = materialDewasa;
                        break;

                     case 2:
                        renderer.material = materialAnak2;
                        break;

                }
            }
        }
    }

    // SCENE POSKO
}
