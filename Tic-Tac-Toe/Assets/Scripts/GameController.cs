using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
    public Button button;
}

[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour
{
    // Buttons added to the buttonList by dragging and dropping the Text fields for all 9 buttons into Game Controller Button List
    public Text[] buttonList;
    private string playerSide;
    public GameObject gameOverPanel;
    public Text gameOverText;
    private int moveCount;
    public GameObject restartButton;

    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;

    public GameObject startInfo;

    void Awake()
    {
        gameOverPanel.SetActive(false);
        SetGameControllerReferenceOnButtons();
        moveCount = 0;
        restartButton.SetActive(false);
    }

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }

    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()
    {
        moveCount++;
        // Check rows
        if (CheckForWin(0, 1, 2)){
            GameOver();
        }
        else if (CheckForWin(3, 4, 5)){
            GameOver();
        }
        else if (CheckForWin(6, 7, 8))
        {
            GameOver();
        }
        // Check columns
        else if (CheckForWin(0, 3, 6))
        {
            GameOver();
        }
        else if (CheckForWin(1, 4, 7))
        {
            GameOver();
        }
        else if (CheckForWin(2, 5, 8))
        {
            GameOver();
        }
        // Check diagonals
        else if (CheckForWin(0, 4, 8))
        {
            GameOver();
        }
        else if (CheckForWin(2, 4, 6))
        {
            GameOver();
        }
        else
        {
            ChangeSides();
        }

        if (moveCount >= 9)
        {
            SetGameOverText("It's a draw!");
        }
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
        CheckPlayerSide();
    }

    public bool CheckForWin(int i, int j, int k)
    {
        string btn1 = buttonList[i].text;
        string btn2 = buttonList[j].text;
        string btn3 = buttonList[k].text;

        return playerSide == btn1 && btn1 == btn2 && btn2 == btn3;
    }

    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;

        restartButton.SetActive(true);
        SetPlayerColorsInactive();
    }

    public void GameOver()
    {
        SetButtonInteractive(false);

        SetGameOverText(playerSide + " Wins!");
    }

    private void SetButtonInteractive(bool b)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = b;
            if (b)
            {
                buttonList[i].text = "";
            }
        }
    }

    public void RestartGame()
    {
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
        startInfo.SetActive(true);
    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    public void SetStartingSide(string startingSide)
    {
        playerSide = startingSide;
        CheckPlayerSide();
        StartGame();
    }

    private void StartGame()
    {
        SetButtonInteractive(true);
        SetPlayerButtons(false);
        startInfo.SetActive(false);
    }

    void CheckPlayerSide()
    {
        if (playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
    }

    void SetPlayerButtons(bool toggle)
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }

    void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }

}