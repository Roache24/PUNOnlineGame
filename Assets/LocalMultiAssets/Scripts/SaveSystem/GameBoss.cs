using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoss : MonoBehaviour
{
    public static GameBoss instance;

    public string P1name;
    public string P2name;

   public GameData saveData = new GameData();

    private void Awake()
    {
        #region Singleton pattern
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion

    }

    

    public void SaveWinner()
    {
        SaveSystem.instance.SaveGame(saveData);
        
    }
    public void WinNumber()
    {
        saveData = SaveSystem.instance.LoadGame();
    }

    public void AddWinsP1()
    {
        saveData.AddWinsP1(1);
        PrintScore();
    }


    public void AddWinsP2()
    {
        saveData.AddWinsP1(2);
        PrintScore();
    }

    public void ResetData()
    {
        saveData.ResetData();
        PrintScore();
    }

    public void PrintScore()
    {
        Debug.Log("Player 1 :" + saveData.WinsP1);
        Debug.Log("Player 2 :" + saveData.WinsP2);
    }

}
