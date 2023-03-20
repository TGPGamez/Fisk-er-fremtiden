using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{
    [SerializeField] private Canvas saveResultCanvas;
    [SerializeField] private Canvas enterNameCanvas;
    [SerializeField] private TMP_InputField input;
    [SerializeField] private GameObject keyboard;
    [SerializeField] private ApiHandler apiHandler;
    [SerializeField] private SceneControl sceneControl;

    private int fishAmount;
    private int fishWeight;
    private int points;

    private void OnEnable()
    {
        fishAmount = PlayerPrefs.GetInt("fishAmount");
        fishWeight = PlayerPrefs.GetInt("fishWeight");
        points = PlayerPrefs.GetInt("points");
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

    public void SubmitResult()
    {
        if (input != null && input.text.Length >= 2)
        {
            UserPoints userPoints = new UserPoints(input.text, fishWeight, fishAmount, points);
            apiHandler.CallPostApiAsync(userPoints);
            sceneControl.SceneChangeWithIndex(0);
        }
    }
}
