using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPawn : PlayerPiece
{
    public void OnMouseDown()
    {
        if (!isReady)
        {
            MakePlayerReadyToMove(pathParent.yellowPathPoint);
            return;
        }
        
        MoveSteps(pathParent.yellowPathPoint);
    }
}
