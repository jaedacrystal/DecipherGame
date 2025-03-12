using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HideTextOnLoad : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Start()
    {
        text.gameObject.SetActive(false);
    }
}
