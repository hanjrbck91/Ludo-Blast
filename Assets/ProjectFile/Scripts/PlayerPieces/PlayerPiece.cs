using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{
    public bool moveNow;
    public int numberOfStepsToMove;
    public PathObjectParent pathParent;

    private void Awake()
    {
        pathParent = FindObjectOfType<PathObjectParent>();
    }
}
