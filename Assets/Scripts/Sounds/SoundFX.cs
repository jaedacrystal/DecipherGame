using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SoundFX : MonoBehaviour
{
    private static SoundFX instance;

    private static AudioSource audioSource;
    private static AudioSource randomPitchAudioSource;
    private static SoundFXLibrary soundEffectLibrary;

    [SerializeField] private Slider slider;

    public static float minPitch = 0.9f;
    public static float maxPitch = 1.2f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            AudioSource[] audioSources = GetComponents<AudioSource>();
            audioSource = audioSources[1];
            randomPitchAudioSource = audioSources[0];

            if (soundEffectLibrary == null)
            {
                soundEffectLibrary = FindObjectOfType<SoundFXLibrary>();
            }
        }
    }

    private void Start()
    {
        slider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public static void Play(string soundName, bool randomPitch = false)
    {
        AudioClip audioClip = soundEffectLibrary.GetRandomClip(soundName);

        if (audioClip != null)
        {
            if (randomPitch)
            {
                randomPitchAudioSource.pitch = Random.Range(minPitch, maxPitch);
                randomPitchAudioSource.PlayOneShot(audioClip);
            }
            else
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
