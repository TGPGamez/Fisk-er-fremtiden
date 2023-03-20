using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

public class ApiHandler : MonoBehaviour
{
    [SerializeField] private string url;


    /// <summary>
    /// Method to post results to our API
    /// </summary>
    /// <param name="userPoints">Player data/points</param>
    public async void CallPostApiAsync(UserPoints userPoints)
    {
        HttpClient httpClient = new HttpClient();
        HttpContent content = new StringContent(JsonConvert.SerializeObject(userPoints), Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await httpClient.PostAsync($"{url}record", content);
            Debug.Log(userPoints.ToString());
        }
        catch (Exception ex)
        {
            Debug.Log($"API ERROR: {ex.Message}");
            throw;
        }
       
    }
}
