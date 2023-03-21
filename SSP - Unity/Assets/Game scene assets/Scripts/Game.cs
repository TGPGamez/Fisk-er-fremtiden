using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    public Slider foodslider;
    public Slider sicknesSlider;
    public Slider waterPollutionSlider;
    public TMP_Text FishAmount;
    public TMP_Text FishWeight;

    [SerializeField] private int fish; 
    [SerializeField] private int fishWeight; 
    [SerializeField] private float food; 
    [SerializeField] private float waterPollution; 
    [SerializeField] private float sickness;

    public UnityEvent AllFishDeadEvent;


    private void OnDisable()
    {
        PlayerPrefs.SetInt("fishAmount", fish);
        PlayerPrefs.SetInt("fishWeight", fishWeight);
        PlayerPrefs.SetInt("points", CalculatePoints());
    }

    private int CalculatePoints()
    {
        return fish * fishWeight;
    }

    /// <summary>
    /// Method for fish "eating" food
    /// </summary>
    public void FishEat()
    {
        //Remove a food percentage
        food -= 2;

        //Checks if food is in optimal range if it is between 50 and 70 it will be optimal grow
        if( food > 50 && food <= 70)
        {
            fishWeight += 50;
            waterPollution += 2;
        }
        //If it is over 70 the water pollution is large but optimal growth
        else if ( food > 70)
        {
            fishWeight += 50;
            waterPollution += 4;
        }
        //If food is between 25 and 50 it will be less than optimal growth with lesser water pollution
        else if (food > 25 && food <= 50)
        {
            fishWeight += 20;
            waterPollution += 1;
        }
        //If under 25 fish will begin to die
        else
        {
            //Calculate survival chance
            float survivalRate = food / 25 * 100;
            //randomise the fish sickness
            int fishSickness = UnityEngine.Random.Range(0, 101);

            //If the fish sickness is higher thant the survival rate kill it
            if (fishSickness > survivalRate)
            {
                //remove fish from pool and add 15 to waterpollution
                fish -= 1;
                waterPollution += 15;
            }
        }

        //Checks the water pollution level. if it is over 20 sickness will be increased with 1 if over 40 it will be 2, over 60 it will be 4, over 80 it will be 8
        if(waterPollution < 20) { }
        else if (waterPollution >= 20 && waterPollution < 40) { sickness += 1; }
        else if (waterPollution >= 40 && waterPollution < 60) { sickness += 2; }
        else if (waterPollution >= 60 && waterPollution < 80) { sickness += 4; }
        else if (waterPollution >= 80) { sickness += 8; }

        //If the sickness if over 15 calculate the rate of survival for the fish
        if (sickness > 15)
        {
            //Fish chance of death
            float deathrate = sickness / 85 * 100;
            //Fish health
            int fishSurvival = UnityEngine.Random.Range(1, 100);
            //if fish health is below death chance
            if (fishSurvival < deathrate)
            {
                //remove fish from pool and add 15 to waterpollution
                fish -= 1;
                waterPollution += 15;
            }
        }

        //Control exceeding values 
        if(waterPollution > 100) { waterPollution = 100; }
        if(sickness > 100) { sickness = 100; }
        if(food < 0) { food = 0; }
        
        //Check if all fish is dead if true queue end event
        if(fish <= 0) 
        {
            if (AllFishDeadEvent != null)
            {
                AllFishDeadEvent.Invoke();
            }
            fish = 0;
        }
        

        //Update visual variables
        foodslider.value = food;
        sicknesSlider.value = sickness;
        waterPollutionSlider.value = waterPollution;
        FishAmount.text = fish.ToString();
        FishWeight.text = fishWeight.ToString();
    }

    /// <summary>
    /// When pond expereience collision
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        //Check if the object is Food or Medicin
        if (other.gameObject.CompareTag("Food"))
        {
            //Feed the fish
            FeedFish(); 
        }
        else if(other.gameObject.CompareTag("Medicin"))
        {
            //Medicinate the fish
            MedicinateFish();
        }
        Destroy(other.gameObject);
    }

    /// <summary>
    /// Feed The fish With 25 points a cant go beyond 100
    /// </summary>
    private void FeedFish()
    {
        food += 25;
        if (food > 100)
        {
            food = 100;
        }
    }

    //Medicinate the fish remove 35 from sickness cant go below 0
    private void MedicinateFish()
    {
        sickness -= 35;
        if (sickness < 0)
        {
            sickness = 0;
        }
    }

    /// <summary>
    /// Clean the water. Remove 50 from water pollution update waterPollutionSlider slider and cant go below 0
    /// </summary>
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
