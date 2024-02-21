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
       
        MoveSteps(pathParent.redPathPoint);
    }
}
