using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataManage : MonoBehaviour
{
    [SerializeField] private ApiHandler apiHandler;
    [SerializeField] private TMP_Text fishText;
    [SerializeField] private TMP_Text weightText;
    [SerializeField] private TMP_Text pointsText;
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
    /// On start, display score
    /// </summary>
    private void Start()
    {
        fishText.text = fishAmount.ToString();
        weightText.text = fishWeight + " gram";
        pointsText.text = points.ToString();
    }



    /// <summary>
    /// Send data to api
    /// </summary>
    public void SendToApi(string name)
    {
        //Create object with data
        UserPoints userPoints = new UserPoints(name, fishWeight, fishAmount, points);
        //Call Api to post result
        apiHandler.CallPostApiAsync(userPoints);
    }
}
