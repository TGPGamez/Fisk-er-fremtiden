using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Timer : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Indicator type;
    private float timeLeft;
    [SerializeField] private UnityEvent endEvent = new UnityEvent();


    void Start()
    {
        timeLeft = CalculatetimeLeft();
    }

    private float elapsed = 0f;
    void Update()
    {
        if (timeLeft > 0)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= 1f)
            {
                elapsed = elapsed % 1f;
                timeLeft--;
                if (timeLeft == 0)
                {
                    endEvent.Invoke();
                }
            }
        }
    }

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
