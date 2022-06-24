using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData 
{
    public int WinsP1 = 0;
    public int WinsP2 = 0;

    public void AddWinsP1(int points)
    {
        WinsP1 += points;
    }

    public void AddWinsP2(int points)
    {
        WinsP2 += points;
    }

    public void ResetData()
    {
        WinsP1 = 0;
        WinsP2 = 0;
    }


}




