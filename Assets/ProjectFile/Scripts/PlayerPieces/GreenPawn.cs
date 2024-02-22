using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPawn : PlayerPiece
{
    RollingDice greenDice;

    private void Start()
    {
        greenDice = GetComponentInParent<GreenHome>().dice;
    }
    public void OnMouseDown()
    {
        if (!System.Object.ReferenceEquals(GameManager.Instance.rollingDice, null))
        {
            if (!isReady)
            {
                if (GameManager.Instance.rollingDice == greenDice && GameManager.Instance.numberOfStepsToMove == 6)
                {
                    MakePlayerReadyToMove(pathParent.greenPathPoint);
                    GameManager.Instance.numberOfStepsToMove = 0;
                    return;
                }

            }
            if (GameManager.Instance.rollingDice == greenDice && isReady && GameManager.Instance.canPlayerMove)
            {
                GameManager.Instance.canPlayerMove = false;
                MoveSteps(pathParent.greenPathPoint);
            }
        }

    }
}
