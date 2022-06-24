using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;

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
        }
        #endregion
    }
    //List of players
    public GameObject[] players;
    //List of scores
    public int[] playerScores;
    //List of spawn positions
    public Transform[] spawnPositions;
  
    //Names
    public string P1Name;
    public string P2Name;

    public int maxKills;
    int winningPlayer;

   public void Start()
   {
       SetUpScene();
        P1Name = GameBoss.instance.P1name;
        P2Name = GameBoss.instance.P2name;
        GameBoss.instance.WinNumber();
        GameBoss.instance.PrintScore();
        
   }

    public void SetUpScene()
    { 
      for (int i = 1; i < players.Length+1; i++)
       {
            SpawnPlayer(i);
       }
 
   }

    
    public void SpawnPlayer(int playerNumber)
    {
        var player = Instantiate(players[playerNumber-1], spawnPositions[playerNumber-1].position, players[playerNumber-1].transform.rotation);

        var playerInputs = player.GetComponent<PlayerInput>();
        playerInputs.playerNum = playerNumber;
        playerInputs.DetermineInputs();

        var playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.canBeDamaged = false;
        playerHealth.Invoke("ResetDamage", 1f);

    }

    public void UpdateScore(int playerScoring, int playerKilled)
    {
        if(playerScoring == 0 || playerScoring == playerKilled)
        {
            playerScores[playerKilled - 1]--;
        }
        else
        {
            playerScores[playerScoring - 1]++;
        }
        RoundUIManager.instance.UpdateScoreUI();
        if (playerScores[playerScoring-1] >= maxKills)
        {
            EndRound(playerScoring);
        }
    }

    public void EndRound(int winningPlayer)
    {
        Cursor.visible = true;
        RoundUIManager.instance.DisplayScores(winningPlayer);
    }

}
