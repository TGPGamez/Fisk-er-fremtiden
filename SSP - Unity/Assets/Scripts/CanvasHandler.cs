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

    private float fishAmount;
    private float fishWeight;
    private float points;

    private void OnEnable()
    {
        fishAmount = PlayerPrefs.GetFloat("fishAmount");
        fishWeight = PlayerPrefs.GetFloat("fishWeight");
        points = PlayerPrefs.GetFloat("points");
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
