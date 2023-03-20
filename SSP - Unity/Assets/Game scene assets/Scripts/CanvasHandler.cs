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

    /// <summary>
    /// When script is loaded then retrieve
    /// player data from game
    /// </summary>
    private void OnEnable()
    {
        fishAmount = PlayerPrefs.GetInt("fishAmount");
        fishWeight = PlayerPrefs.GetInt("fishWeight");
        points = PlayerPrefs.GetInt("points");
    }

    /// <summary>
    /// Method to determine next action
    /// if status is 1, then we continue to entering of name/username
    /// otherwise just go back to start menu
    /// </summary>
    /// <param name="status"></param>
    public void SaveResultClick(int status)
    {
        if (status == 0)
        {
            SceneManager.LoadScene(0);
        } else if (status == 1)
        {
            //Disable current displaying canvas
            saveResultCanvas.GetComponent<Canvas>().enabled = false;
            //Enable canvas display where player have to enter name/username
            enterNameCanvas.GetComponent<Canvas>().enabled = true;
            keyboard.SetActive(true);
        }
    }

    /// <summary>
    /// Method to submit result after player
    /// have entered name/username
    /// </summary>
    public void SubmitResult()
    {
        if (input != null && input.text.Length >= 2)
        {
            //Create object with data
            UserPoints userPoints = new UserPoints(input.text, fishWeight, fishAmount, points);
            //Call Api to post result
            apiHandler.CallPostApiAsync(userPoints);
            //Change back to start scene
            sceneControl.SceneChangeWithIndex(0);
        }
    }
}
