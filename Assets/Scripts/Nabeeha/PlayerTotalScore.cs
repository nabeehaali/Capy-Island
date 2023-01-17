using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class PlayerTotalScore : IComparable<PlayerTotalScore>
{
    public string playerID;
    public int totalPoints;
    public bool chefHat;
    public bool wizardHat;
    public bool hockeyHat;
    public bool lastHat;

    public PlayerTotalScore(string newplayerID, int newtotalPoints)
    {
        playerID = newplayerID;
        totalPoints = newtotalPoints;
    }

    public int CompareTo(PlayerTotalScore other)
    {
        if (other == null)
        {
            return 1;
        }

        return totalPoints - other.totalPoints;
    }
}

public class TotalEqualityComparer : IEqualityComparer<PlayerTotalScore>
{
    public bool Equals(PlayerTotalScore x, PlayerTotalScore y)
    {
        // Two items are equal if their keys are equal.
        return x.totalPoints == y.totalPoints;
    }

    public int GetHashCode(PlayerTotalScore obj)
    {
        return obj.totalPoints.GetHashCode();
    }
}

