using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_Posko : MonoBehaviour
{

    public static int usia = 0;
    [SerializeField] Material materialDewasa;
    [SerializeField] Material materialAnak2;
    [SerializeField] GameObject[] objekUntukDiubah;

    public Transform cameraRig; // XR Origin atau XR Rig
    public float adultHeight = 1.7f; // Tinggi dewasa
    public float childHeight = 1.4f;
    private void Start()
    {
        GantiMaterialTangan();
        if (usia == 1) 
        {
            //SetCameraHeight(true);
            GantiMaterialTangan();
        }
        else
        {
            GantiMaterialTangan();
            //SetCameraHeight(false);
        }
    }
    // SCENE MAIN MENU
    public void PilihDewasa()
    {
        usia = 1;
        //LoadScene_posko();
        GantiMaterialTangan();
        SetCameraHeight(true);
    }

    public void PilihAnak2()
    {
        usia = 2;
        //LoadScene_posko();
        GantiMaterialTangan();
        SetCameraHeight(false);
    }
    public void SetCameraHeight(bool isAdult)
    {
        float targetHeight = isAdult ? adultHeight : childHeight;
        Vector3 newPosition = cameraRig.position;
        newPosition.y = targetHeight;
        cameraRig.position = newPosition;
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
