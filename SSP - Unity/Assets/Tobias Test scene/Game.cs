using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public Slider foodslider;
    public Slider sicknesSlider;
    public Slider waterPollutionSlider;
    public TMP_Text FishAmount;
    public TMP_Text FishWeight;

    [SerializeField] private float fish; 
    [SerializeField] private float fishWeight; 
    [SerializeField] private float food; 
    [SerializeField] private float waterPollution; 
    [SerializeField] private float sickness; 

    public void StartScene()
    {
        
    }

    public void ResetScene()
    {
       
    }

    public void EndScene()
    {

    }

    public void FishEat()
    {
        food -= 1 ;
        if( food > 50 && food <= 70)
        {
            fishWeight += 50;
            waterPollution += 2;
        }
        else if ( food > 70)
        {
            fishWeight += 50;
            waterPollution += 4;
        }
        else if (food > 25 && food <= 50)
        {
            fishWeight += 20;
            waterPollution += 1;
        }
        else
        {
            float survivalRate = food / 25 * 100;
            int fishSurvival = Random.Range(0, 101);
            if (fishSurvival > survivalRate)
            {
                fish -= 1;
                waterPollution += 15;
            }
        }

        if(waterPollution < 20) { }
        else if (waterPollution >= 20 && waterPollution < 40) { sickness += 1; }
        else if (waterPollution >= 40 && waterPollution < 60) { sickness += 2; }
        else if (waterPollution >= 60 && waterPollution < 80) { sickness += 4; }
        else if (waterPollution >= 80) { sickness += 8; }

        if (sickness > 15)
        {
            float deathrate = sickness / 85 * 100;
            int fishSurvival = Random.Range(1, 100);
            if (fishSurvival < deathrate)
            {
                fish -= 1;
                waterPollution += 15;
            }
        }

        if(waterPollution > 100) { waterPollution = 100; }
        if(sickness > 100) { sickness = 100; }
        if(food < 0) { food = 0; }
        if(fish < 0) { fish = 0; }
        foodslider.value = food;
        sicknesSlider.value = sickness;
        waterPollutionSlider.value = waterPollution;
        FishAmount.text = fish.ToString();
        FishWeight.text = fishWeight.ToString();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            FeedFish(); 
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("Medicin"))
        {
            MedicinateFish();
            Destroy(other.gameObject);
        }
    }

    private void FeedFish()
    {
        food += 25;
        if (food > 100)
        {
            food = 100;
        }
    }

    private void MedicinateFish()
    {
        sickness -= 35;
        if (sickness < 0)
        {
            sickness = 0;
        }
    }

    public void cleanWater()
    {
        waterPollution -= 50;
        if(waterPollution < 0)
        {
            waterPollution = 0;
        }
        waterPollutionSlider.value = waterPollution;
    }
}
