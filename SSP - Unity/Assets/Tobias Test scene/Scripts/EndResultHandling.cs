using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndResultHandling : MonoBehaviour
{
    [SerializeField] private Canvas saveResultCanvas;
    [SerializeField] private Canvas enterNameCanvas;
    [SerializeField] private TMP_InputField input;
    [SerializeField] private GameObject keyboard;

    public void ShowSaveResultCanvas()
    {
        saveResultCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void SaveResultClick(int status)
    {
        if (status == 0)
        {
            SceneManager.LoadScene(0);
        } else if (status == 1)
        {
            saveResultCanvas.GetComponent<Canvas>().enabled = false;
            enterNameCanvas.GetComponent<Canvas>().enabled = true;
            keyboard.SetActive(true);
        }
    }
}
