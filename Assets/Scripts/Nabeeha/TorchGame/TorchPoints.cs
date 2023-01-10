using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class TorchPoints : IComparable<TorchPoints>
{
    public string playerID;
    public int playerPoints;

    public TorchPoints(string newplayerID, int newplayerPoints)
    {
        playerID = newplayerID;
        playerPoints = newplayerPoints;
    }

    public int CompareTo(TorchPoints other)
    {
        if (other == null)
        {
            return 1;
        }

        return playerPoints - other.playerPoints;
    }
}

public class ItemEqualityComparer : IEqualityComparer<TorchPoints>
{
    public bool Equals(TorchPoints x, TorchPoints y)
    {
        // Two items are equal if their keys are equal.
        return x.playerPoints == y.playerPoints;
    }

    public int GetHashCode(TorchPoints obj)
    {
        return obj.playerPoints.GetHashCode();
    }
}
