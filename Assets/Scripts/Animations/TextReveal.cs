using System.Collections;
using TMPro;
using UnityEngine;

public class TextReveal : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float revealSpeed;
    public float punctuationPause;
    public AudioClip typewriterSound;
    private AudioSource audioSource;
    private Coroutine revealCoroutine;

    private void Start()
    {
        if (textComponent == null)
        {
            textComponent = GetComponent<TextMeshProUGUI>();
            if (textComponent == null)
            {
                Debug.LogError("TextMeshProUGUI component is missing!");
            }
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    public void StartReveal(string text)
    {
        if (revealCoroutine != null)
        {
            StopCoroutine(revealCoroutine);
            if (audioSource != null) audioSource.Stop();
        }

        textComponent.text = "";
        revealCoroutine = StartCoroutine(RevealText(text));
    }

    private IEnumerator RevealText(string fullText)
    {
        if (audioSource != null) audioSource.Stop();

        for (int i = 0; i <= fullText.Length; i++)
        {
            if (textComponent != null)
            {
                textComponent.text = fullText.Substring(0, i);
            }
            else
            {
                Debug.LogError("textComponent is NULL during text reveal!");
                yield break;
            }

            if (i > 0)
            {
                PlayTypingSound();

                if (Punctuation(fullText[i - 1]))
                {
                    yield return new WaitForSeconds(punctuationPause);
                }
                else
                {
                    yield return new WaitForSeconds(revealSpeed);
                }
            }
        }

        if (audioSource != null) audioSource.Stop();
    }

    private void PlayTypingSound()
    {
        if (typewriterSound != null && audioSource != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(typewriterSound);
            }
        }
    }

    private bool Punctuation(char c)
    {
        return c == '.' || c == ',' || c == '!' || c == '?';
    }
}
