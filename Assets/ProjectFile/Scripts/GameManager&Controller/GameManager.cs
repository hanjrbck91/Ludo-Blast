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

    private void Awake()
    {
        Instance = this;
    }
}
