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
        
        MoveSteps(pathParent.bluePathPoint);
    }
   
}
