using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPoints
{
    public UserPoints(string name, float highestWeight, float mostFish, float points)
    {
        Name = name;
        HighestWeight = highestWeight;
        MostFish = mostFish;
        Points = points;
    }

    public string Name { get; set; }
    public float HighestWeight { get; set; }
    public float MostFish { get; set; }
    public float Points { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Weight: {HighestWeight}, Fish amount: {MostFish}, Points: {Points}";
    }
}
