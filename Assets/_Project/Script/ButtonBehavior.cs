using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio()
    {
        audioSource.Play();
        Debug.Log("AudioNgeplay");
    }
}
