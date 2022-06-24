using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScreen : MonoBehaviour
{
    public TMP_Text winsP1Box;
    public TMP_Text winsP2Box;

    public void CalculateScore()
    {
        winsP1Box.text = GameBoss.instance.saveData.WinsP1.ToString();
        winsP2Box.text = GameBoss.instance.saveData.WinsP2.ToString();

    }

    public void LoadScores() 
    {
        GameBoss.instance.WinNumber();
    }

    public void ResetScores()
    {
        GameBoss.instance.ResetData();
        GameBoss.instance.SaveWinner();
    }

}
