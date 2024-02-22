using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePawn : PlayerPiece
{
    RollingDice blueDice;

    private void Start()
    {
        blueDice = GetComponentInParent<BlueHome>().dice;
    }
    public void OnMouseDown()
    {
        if(!System.Object.ReferenceEquals(GameManager.Instance.rollingDice,null))
        {
            if (!isReady)
            {
                if(GameManager.Instance.rollingDice == blueDice && GameManager.Instance.numberOfStepsToMove == 6)
                {
                    MakePlayerReadyToMove(pathParent.bluePathPoint);
                    GameManager.Instance.numberOfStepsToMove = 0;
                    return;
                }
                
            }
            if (GameManager.Instance.rollingDice == blueDice && isReady && GameManager.Instance.canPlayerMove)
            {
                GameManager.Instance.canPlayerMove = false;
                MoveSteps(pathParent.bluePathPoint);
            }
        }
       
    }
   
}
