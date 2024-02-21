using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPawn : PlayerPiece
{
    public void OnMouseDown()
    {
        if (!isReady)
        {
            MakePlayerReadyToMove(pathParent.greenPathPoint);
            return;
        }
        numberOfStepsToMove = 5;
        MoveSteps(pathParent.greenPathPoint);
    }
}
