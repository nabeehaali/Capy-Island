using System.Collections;
using System.Collections.Generic;
using System;

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
