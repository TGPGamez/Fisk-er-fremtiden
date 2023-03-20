using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to store data from game
/// </summary>
public class UserPoints
{
    public UserPoints(string name, int highestWeight, int mostFish, int points)
    {
        Name = name;
        HighestWeight = highestWeight;
        MostFish = mostFish;
        Points = points;
    }

    public string Name { get; set; }
    public int HighestWeight { get; set; }
    public int MostFish { get; set; }
    public int Points { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Weight: {HighestWeight}, Fish amount: {MostFish}, Points: {Points}";
    }
}
