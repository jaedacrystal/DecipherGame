using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartStory : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("Bedroom", LoadSceneMode.Single);
    }
}
