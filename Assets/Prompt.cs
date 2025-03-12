using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    public GameObject prompt;
    void Start()
    {
        prompt.gameObject.SetActive(false);
    }
}
