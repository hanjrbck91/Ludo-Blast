using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{
    public bool isReady;
    public bool moveNow;
    public int numberOfStepsToMove;
    public int numberOfStepsAlreadyMoved;
    public PathObjectParent pathParent;

    private void Awake()
    {
        pathParent = FindObjectOfType<PathObjectParent>();
    }

    public void MoveSteps(PathPoint[] pathPointsToMoveon_)
    {
        StartCoroutine(MoveSteps_Enum(pathPointsToMoveon_));
    }

    public void MakePlayerReadyToMove(PathPoint[] pathPointsToMoveon_)
    {
        isReady = true;
        transform.position = pathPointsToMoveon_[0].transform.position;
        numberOfStepsAlreadyMoved = 1;
    }
    IEnumerator MoveSteps_Enum(PathPoint[] pathPointsToMoveon_)
    {
        // Accroding to the dice we need to move the pawn
        numberOfStepsToMove = GameManager.Instance.numberOfStepsToMove;

        for (int i = numberOfStepsAlreadyMoved; i < (numberOfStepsAlreadyMoved + numberOfStepsToMove); i++)
        {
            transform.position = pathPointsToMoveon_[i].transform.position;

            yield return new WaitForSeconds(0.35f);
        }
        numberOfStepsAlreadyMoved += numberOfStepsToMove;
    }
}
