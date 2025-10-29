using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Score.cs
public class Score
{
    public int points;
    public string playerName;

    public Score(int points, string playerName)
    {
        this.points = points;
        this.playerName = playerName;
    }

    public override string ToString()
    {
        return $"{points} - {playerName}";
    }
}

