using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class KeyboardButton : MonoBehaviour
{
    private Keyboard keyboard;
    private TextMeshProUGUI buttonText;

    private void Start()
    {
        keyboard = GetComponentInParent<Keyboard>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText.text.Length == 1)
        {
            NameToButtonText();
            GetComponentInChildren<ButtonVR>().onRelease.AddListener(delegate { keyboard.InsertChar(buttonText.text); });
        }
    }
    public void NameToButtonText()
    {
        buttonText.text = gameObject.name;
    }
}
