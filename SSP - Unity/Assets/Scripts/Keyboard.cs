using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keyboard : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject normalButtons;
    [SerializeField] private GameObject capsButton;
    private bool caps;

    private void Start()
    {
        caps = false;
    }

    public void InsertChar(string c)
    {
        inputField.text += c;
    }

    public void DeleteChar()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }

    public void InsertSpace()
    {
        inputField.text += " ";
    }

    public void CapsPressed()
    {
        if (!caps)
        {
            normalButtons.SetActive(false);
            capsButton.SetActive(true);
            caps = true;
        } else
        {
            capsButton.SetActive(false);
            normalButtons.SetActive(true);
            caps = false;
        }
    }
}
