using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------- Audio Source -------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------- Audio Clip -------")]
    public AudioClip background;
    public AudioClip doorOpen;
    public AudioClip doorClose;
    public AudioClip fall;
    public AudioClip changeScene;
    public AudioClip earthquake;
    public AudioClip footStep;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }



}