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
}
