using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class MinigamePoints : IComparable<MinigamePoints>
{
    public string playerID;
    public int playerPoints;

    public MinigamePoints(string newplayerID, int newplayerPoints)
    {
        playerID = newplayerID;
        playerPoints = newplayerPoints;
    }

    public int CompareTo(MinigamePoints other)
    {
        if (other == null)
        {
            return 1;
        }

        return playerPoints - other.playerPoints;
    }
}

public class ItemEqualityComparer : IEqualityComparer<MinigamePoints>
{
    public bool Equals(MinigamePoints x, MinigamePoints y)
    {
        // Two items are equal if their keys are equal.
        return x.playerPoints == y.playerPoints;
    }

    public int GetHashCode(MinigamePoints obj)
    {
        return obj.playerPoints.GetHashCode();
    }
}
