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
        
        MoveSteps(pathParent.greenPathPoint);
    }
}
