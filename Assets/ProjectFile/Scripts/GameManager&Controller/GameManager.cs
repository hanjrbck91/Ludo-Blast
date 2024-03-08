using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance;
    public int numberOfStepsToMove;
    public RollingDice rollingDice;
    // bool to stop multiple palyer movement in one dice roll due to coroutine 
    public bool canPlayerMove = true;

    // List to store pawns on the pathpoint
    List<PathPoint> playerOnPathPointList = new List<PathPoint>();

    public bool canDiceRoll = true;
    public bool transferDice = false;
    public bool selfDice = false;

    public int blueOutPlayers;
    public int redOutPlayers;
    public int yellowOutPlayers;
    public int greenOutPlayers;

    public int blueCompletePlayers;
    public int redCompletePlayers;
    public int yellowCompletePlayers;
    public int greenCompletePlayers;

    public RollingDice[] manageRollingDice;

    public PlayerPiece[] bluePlayerPiece;
    public PlayerPiece[] redPlayerPiece;
    public PlayerPiece[] yellowPlayerPiece;
    public PlayerPiece[] greenPlayerPiece;

    public int totalPlayerCanPlay;


    private void Awake()
    {
        Instance = this;
    }

    public void AddPathPoint(PathPoint pathPoint)
    {
        playerOnPathPointList.Add(pathPoint);
    }

    public void RemovePathPoints(PathPoint pathPoint)
    {
        if(playerOnPathPointList.Contains(pathPoint))
        {
            playerOnPathPointList.Remove(pathPoint);
        }
        else
        {
            Debug.Log("Path point to not found to be removed");
        }
    }

    public void RollingDiceManager()
    {
        if(GameManager.Instance.transferDice)
        {
            if(GameManager.Instance.transferDice)
            {
                ShiftDice();
            }
            
            GameManager.Instance.canDiceRoll = true;
        }
        else
        {
            if(GameManager.Instance.selfDice)
            {
                GameManager.Instance.selfDice = false;
                GameManager.Instance.canDiceRoll = true;
                GameManager.Instance.SelfRoll();
            }
        }
    }

    public void SelfRoll()
    {
        if(GameManager.Instance.totalPlayerCanPlay == 1 && GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[2])
        {
            Invoke("Rolled", 0.6f);
        }
    }

    public void Rolled()
    {
        GameManager.Instance.manageRollingDice[2].MouseRoll();
    }

    private void ShiftDice()
    {

        int nextDice;

        if (GameManager.Instance.totalPlayerCanPlay == 1)
        {
            if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[0])
            {
                GameManager.Instance.manageRollingDice[0].gameObject.SetActive(false);
                GameManager.Instance.manageRollingDice[2].gameObject.SetActive(true);

                passOut(0);

                GameManager.Instance.manageRollingDice[2].MouseRoll();
            }
            else
            {
                GameManager.Instance.manageRollingDice[0].gameObject.SetActive(true);
                GameManager.Instance.manageRollingDice[2].gameObject.SetActive(false);
                passOut(2);
            }
        }
        else if(GameManager.Instance.totalPlayerCanPlay == 2)
        {
            if(GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[0])
            {
                GameManager.Instance.manageRollingDice[0].gameObject.SetActive(false);
                GameManager.Instance.manageRollingDice[2].gameObject.SetActive(true);
                passOut(0);
            }
            else
            {
                GameManager.Instance.manageRollingDice[0].gameObject.SetActive(true);
                GameManager.Instance.manageRollingDice[2].gameObject.SetActive(false);
                passOut(2);
            }
        }
        else if(GameManager.Instance.totalPlayerCanPlay ==3)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == 2) { nextDice = 0; } else { nextDice = i + 1; }
                i = passOut(i);
                if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[i])
                {
                    GameManager.Instance.manageRollingDice[i].gameObject.SetActive(false);
                    GameManager.Instance.manageRollingDice[nextDice].gameObject.SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == 3) { nextDice = 0; } else { nextDice = i + 1; }
                i = passOut(i);
                if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[i])
                {
                    GameManager.Instance.manageRollingDice[i].gameObject.SetActive(false);
                    GameManager.Instance.manageRollingDice[nextDice].gameObject.SetActive(true);
                }
            }
        }
    }

    private int passOut(int i)
    {
        if (i == 0) { if (GameManager.Instance.blueCompletePlayers == 4) { return (i + 1); } }
        else if (i == 1) { if(GameManager.Instance.blueCompletePlayers == 4) { return (i + 1); } }
        else if (i == 2) { if (GameManager.Instance.blueCompletePlayers == 4) { return (i + 1); } }
        else if (i == 3) { if (GameManager.Instance.blueCompletePlayers == 4) { return (i + 1); } }
        
        return i;
    }
}
