using TMPro;
using UnityEngine;

public class Text : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextReveal textReveal;

    void Start()
    {
        textReveal.StartReveal(text.text);
    }
}
