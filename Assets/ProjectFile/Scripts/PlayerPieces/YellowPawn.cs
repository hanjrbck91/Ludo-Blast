using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPawn : PlayerPiece
{
    RollingDice yellowDice;

    private void Start()
    {
        yellowDice = GetComponentInParent<YellowHome>().dice;
    }
    public void OnMouseDown()
    {
        if (!System.Object.ReferenceEquals(GameManager.Instance.rollingDice, null))
        {
            if (!isReady)
            {
                if (GameManager.Instance.rollingDice == yellowDice && GameManager.Instance.numberOfStepsToMove == 6)
                {
                    // number out player in the scene
                    GameManager.Instance.yellowOutPlayers += 1;

                    MakePlayerReadyToMove(pathParent.yellowPathPoint);
                    GameManager.Instance.numberOfStepsToMove = 0;
                    return;
                }

            }
            if (GameManager.Instance.rollingDice == yellowDice && isReady && GameManager.Instance.canPlayerMove )
            {
                GameManager.Instance.canPlayerMove = false;
                MoveSteps(pathParent.yellowPathPoint);
            }
        }

    }
}
