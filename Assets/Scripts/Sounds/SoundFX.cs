using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundFX : MonoBehaviour
{
    private static SoundFX instance;

    private static AudioSource audioSource;
    private static AudioSource randomPitchAudioSource;
    private static SoundFXLibrary soundEffectLibrary;
    [SerializeField] private Slider slider;

    private void Awake()
    {
        if (instance == null)
        {
            AudioSource[] audioSources = GetComponents<AudioSource>();
            audioSource = audioSources[0];
            randomPitchAudioSource = audioSources[0];
            soundEffectLibrary = GetComponent<SoundFXLibrary>();
        }
        
    }

    private void Start()
    {
        slider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public static void Play(string soundName, bool randomPitch = false)
    {
        AudioClip audioClip = soundEffectLibrary.GetRandomClip(soundName);

        if(audioClip != null)
        {
            if(randomPitch)
            {
                randomPitchAudioSource.pitch = Random.Range(1f, 1.5f);
                randomPitchAudioSource.PlayOneShot(audioClip);
            } else
            {
                audioSource.PlayOneShot(audioClip);
            }
                
        }
    }

    public static void SetVolume(float volume)
    {
        audioSource.volume = volume;
        randomPitchAudioSource.volume = volume;
    }

    public void OnValueChanged()
    {
        SetVolume(slider.value);
    }
}
