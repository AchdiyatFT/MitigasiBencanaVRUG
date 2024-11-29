using UnityEngine;

public class EarthquakeButton : MonoBehaviour
{

    [SerializeField] GameManager gameManager;
    public void start_earthquake()
    {
        gameManager.Earthquake = true;
    }

    public void end_earthquake()
    {
        gameManager.Earthquake = false;
    }
}
