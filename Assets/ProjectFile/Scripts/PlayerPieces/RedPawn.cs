using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPawn : PlayerPiece
{
    public void OnMouseDown()
    {
        if (!isReady)
        {
            MakePlayerReadyToMove(pathParent.redPathPoint);
            return;
        }
        numberOfStepsToMove = 5;
        MoveSteps(pathParent.redPathPoint);
    }
}
