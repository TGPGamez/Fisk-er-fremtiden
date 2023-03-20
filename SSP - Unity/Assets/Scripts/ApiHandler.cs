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
    private HttpClient httpClient;

    private void Start()
    {
        httpClient = new HttpClient();
    }


    //public List<UserPoints> GetTop(string category, int amount)
    //{
    //    return CallGetApiAsync($"{category}/{amount}").Result;
    //}

    //private async Task<List<UserPoints>> CallGetApiAsync(string parameters)
    //{
    //    HttpResponseMessage response = await httpClient.GetAsync(url + parameters);
    //    if (response.IsSuccessStatusCode)
    //    {
    //        string json = await response.Content.ReadAsStringAsync();
    //        List<UserPoints> userPoints = JsonConvert.DeserializeObject<List<UserPoints>>(json);
    //        return userPoints;
    //    }
    //    return new List<UserPoints>();
    //}

    public async void CallPostApiAsync(UserPoints userPoints)
    {
        HttpContent content = new StringContent(JsonConvert.SerializeObject(userPoints), Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await httpClient.PostAsync($"{url}record", content);
            Debug.Log(response.RequestMessage);
            Debug.Log(response.StatusCode);
        }
        catch (Exception ex)
        {
            Debug.Log($"ERROR: {ex.Message}");
            throw;
        }
       
    }
}
