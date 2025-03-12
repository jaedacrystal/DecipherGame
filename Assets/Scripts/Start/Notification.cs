using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public PromptDialogue dialogue;
    private AudioSource audioSource;
    public AudioClip sound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    public void ShowDialogue()
    {
        dialogue.showDialogue();
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(sound);
    }
}
