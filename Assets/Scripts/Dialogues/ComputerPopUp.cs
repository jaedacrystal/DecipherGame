using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ComputerPopUp : MonoBehaviour
{
    public Light2D computerLight;
    public Collider2D lightTrigger;
    public Collider2D popupTrigger;
    public Canvas canvas;

    public LevelLoader start;

    private bool isPlayerInside = false;
    private bool soundPlayed = false;

    void Start()
    {
        computerLight.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
        start = FindObjectOfType<LevelLoader>();
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown("space"))
        {
            start.LoadNextScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.IsTouching(lightTrigger) && !soundPlayed)
            {
                computerLight.gameObject.SetActive(true);
                SoundFX.Play("Ping");
                soundPlayed = true;
            }

            if (collision.IsTouching(popupTrigger))
            {
                canvas.gameObject.SetActive(true);
                isPlayerInside = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!collision.IsTouching(popupTrigger))
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }
}
