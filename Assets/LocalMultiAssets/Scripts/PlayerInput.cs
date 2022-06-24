using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInput : MonoBehaviour
{
    public int playerNum = 0;
    [HideInInspector]
    public KeyCode fire, jump, taunt, interact;
    [HideInInspector]
    public string vertical, horizontal;

    private void Awake()
    {
        DetermineInputs();

    }



    public void DetermineInputs()
    {
        switch (playerNum)
        {
            case 1:
                vertical = "P1Vertical";
                horizontal = "P1Horizontal";
                fire = KeyCode.F;
                jump = KeyCode.G;
                taunt = KeyCode.E;
                interact = KeyCode.LeftAlt;
                break;

            case 2:
                vertical = "P2Vertical";
                horizontal = "P2Horizontal";
                fire = KeyCode.RightShift;
                jump = KeyCode.Slash;
                taunt = KeyCode.RightAlt;
                interact = KeyCode.RightControl;
                break;


        }
    }
}


    
