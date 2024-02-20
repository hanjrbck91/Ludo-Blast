using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePawn : PlayerPiece
{
    // Start is called before the first frame update
    void Start()
    {
       // MovePlayer();
       StartCoroutine(MoveSteps_Enum());
    }

    public void MovePlayer()
    {
       
    }

    IEnumerator MoveSteps_Enum()
    {
        for (int i = 0; i < 3; i++)
        {
            transform.position = pathParent.commonPathPoints[i].transform.position;

            yield return new WaitForSeconds(0.35f);
        }
    }
}
