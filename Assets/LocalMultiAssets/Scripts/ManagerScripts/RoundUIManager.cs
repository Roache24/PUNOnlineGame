using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoundUIManager : MonoBehaviour


{
    public static RoundUIManager instance;

    private void Awake()
    {
        #region Singletonn Pattern
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

    [SerializeField] TMP_Text[] playerScoreUIs;
    [SerializeField] CanvasGroup winScreen;
    [SerializeField] TMP_Text winningPlayerName;
    

    public void UpdateScoreUI()
    {
        for (int i = 0; i < playerScoreUIs.Length; i++)
        {
            playerScoreUIs[0].text = RoundManager.instance.P1Name + " : " + RoundManager.instance.playerScores[0].ToString();
            playerScoreUIs[1].text = RoundManager.instance.P2Name + " : " + RoundManager.instance.playerScores[1].ToString();
        }

    }

    public void DisplayScores(int winningPlayer)
    {
        if (winningPlayer == 1)
        {
            winningPlayerName.text = RoundManager.instance.P1Name;
            GameBoss.instance.AddWinsP1();
        }
        if (winningPlayer == 2)
        {
            winningPlayerName.text = RoundManager.instance.P2Name;
            GameBoss.instance.AddWinsP2();
        }
        GameBoss.instance.SaveWinner();
        winScreen.gameObject.SetActive(true);
        StartCoroutine(CanvasFadeIn());

        IEnumerator CanvasFadeIn()
        {
            WaitForEndOfFrame WFEOF = new WaitForEndOfFrame();
            while (winScreen.alpha < 0.99)
            {
                winScreen.alpha = Mathf.Lerp(winScreen.alpha, 1, 0.01f);
                yield return WFEOF;
            }
            yield return null;

        }
    }

}
