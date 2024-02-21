using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePawn : PlayerPiece
{
    public void OnMouseDown()
    {
        if(!isReady)
        {
            MakePlayerReadyToMove(pathParent.bluePathPoint);
            return;
        }
        numberOfStepsToMove = 5;
        MoveSteps(pathParent.bluePathPoint);
    }
   
}
