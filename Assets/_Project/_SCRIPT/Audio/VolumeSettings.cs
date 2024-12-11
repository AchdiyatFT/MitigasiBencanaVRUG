using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class VolumeSettings : MonoBehaviour
{
    public float volume;
    public AudioMixer mixer;

    public void SetVolume(float volume)
    {
        mixer.SetFloat("Volume", volume);
    }
}
