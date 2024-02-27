using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPawn : PlayerPiece
{
    RollingDice redDice;

    private void Start()
    {
        redDice = GetComponentInParent<RedHome>().dice;
    }
    public void OnMouseDown()
    {
        if (!System.Object.ReferenceEquals(GameManager.Instance.rollingDice, null))
        {
            if (!isReady)
            {
                if (GameManager.Instance.rollingDice == redDice && GameManager.Instance.numberOfStepsToMove == 6)
                {
                    // number out player in the scene
                    GameManager.Instance.redOutPlayers += 1;

                    MakePlayerReadyToMove(pathParent.redPathPoint);
                    GameManager.Instance.numberOfStepsToMove = 0;
                    return;
                }

            }
            if (GameManager.Instance.rollingDice == redDice && isReady && GameManager.Instance.canPlayerMove)
            {
                GameManager.Instance.canPlayerMove = false;
                MoveSteps(pathParent.redPathPoint);
            }
        }

    }
}
