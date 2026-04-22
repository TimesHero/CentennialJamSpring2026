using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Music Audio Clips")]
    public AudioClip Music;

    [Header("SFX Audio Clips")]
    public AudioClip pauseSound;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSource.clip = Music;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void PlaySFXRandomPitch(AudioClip clip)
    {
        SFXSource.pitch = Random.Range(0.7f, 1.3f);
        SFXSource.PlayOneShot(clip);
    }
}
