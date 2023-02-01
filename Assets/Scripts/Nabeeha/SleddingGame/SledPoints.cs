using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
public class SledPoints : IComparable<SledPoints>
{
    public string playerID;
    public int playerPoints;

    public SledPoints(string newplayerID, int newplayerPoints)
    {
        playerID = newplayerID;
        playerPoints = newplayerPoints;
    }

    public int CompareTo(SledPoints other)
    {
        if (other == null)
        {
            return 1;
        }

        return playerPoints - other.playerPoints;
    }
}

public class ItemEqualityComparerSled : IEqualityComparer<SledPoints>
{
    public bool Equals(SledPoints x, SledPoints y)
    {
        // Two items are equal if their keys are equal.
        return x.playerPoints == y.playerPoints;
    }

    public int GetHashCode(SledPoints obj)
    {
        return obj.playerPoints.GetHashCode();
    }
}
