using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Indicator type;
    [SerializeField] private float timeLeft;
    [SerializeField] private UnityEvent endEvent = new UnityEvent();
    [SerializeField] private UnityEvent everySecondEvent = new UnityEvent();
    public TMP_Text visualtimer;


    void Start()
    {
        //Calculate time
        timeLeft = CalculatetimeLeft();
    }

    private float elapsed = 0f;
    void Update()
    {
        if (timeLeft > 0)
        {
            //Get delta time from Unity
            elapsed += Time.deltaTime;
            //Determine if a 'second' has reached
            if (elapsed >= 1f)
            {
                //Reset elapsed
                elapsed = elapsed % 1f;
                //Subtract from timeLeft
                timeLeft--;

                TimeSpan time = TimeSpan.FromSeconds(timeLeft);
                //If there is a visual timer then update it to new time left
                if (visualtimer != null)
                {
                    visualtimer.text = string.Format($"{time.Minutes}:{time.Seconds}");
                }
                EventHandle();
            }
        }
    }

    /// <summary>
    /// Method to execute our UnityEvents that
    /// happens every second and when timer is done
    /// </summary>
    private void EventHandle()
    {
        if (everySecondEvent != null)
        {
            everySecondEvent.Invoke();
        }
        if (timeLeft == 0)
        {
            if (endEvent != null)
            {
                endEvent.Invoke();
            }
        }
    }

    /// <summary>
    /// Calculate the time left in seconds, out from the
    /// type of indicator there is selected and the time
    /// </summary>
    /// <returns>Calculated time in seconds</returns>
    private float CalculatetimeLeft()
    {
        switch (type)
        {
            case Indicator.Seconds:
                return time;
            case Indicator.Minutes:
                return time * 60;
            case Indicator.Hours:
                return (time * 60) * 60;
            default:
                break;
        }
        return 0;
    }
}

public enum Indicator
{
    Seconds,
    Minutes,
    Hours
}
