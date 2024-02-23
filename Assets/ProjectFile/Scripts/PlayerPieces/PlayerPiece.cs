using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerPiece : MonoBehaviour
{
    public bool isReady;
    public bool moveNow;
    public int numberOfStepsToMove;
    public int numberOfStepsAlreadyMoved;
    public PathObjectParent pathParent;

    Coroutine MovePlayerPiece;

    private void Awake()
    {
        pathParent = FindObjectOfType<PathObjectParent>();
    }

    public void MoveSteps(PathPoint[] pathPointsToMoveon_)
    {
        MovePlayerPiece = StartCoroutine(MoveSteps_Enum(pathPointsToMoveon_));
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
            if (isPathPointsAvailableToMove(numberOfStepsToMove, numberOfStepsAlreadyMoved, pathPointsToMoveon_))
            {
                transform.position = pathPointsToMoveon_[i].transform.position;

                yield return new WaitForSeconds(0.35f);
            }
        }

        if (isPathPointsAvailableToMove(numberOfStepsToMove, numberOfStepsAlreadyMoved, pathPointsToMoveon_))
        {
            numberOfStepsAlreadyMoved += numberOfStepsToMove;
            GameManager.Instance.numberOfStepsToMove = 0;
        }

        GameManager.Instance.canPlayerMove = true;

        if (!System.Object.ReferenceEquals(MovePlayerPiece, null))
        {
            StopCoroutine("MoveSteps_Enum");
        }
    }

    // To Check wheather player pawn has space to move in the pathpoint or not 
    bool isPathPointsAvailableToMove(int numOfStepsToMove, int numOfStepsAlreadyMoved, PathPoint[] pathPointsToMoveon_)
    {
        if (numberOfStepsToMove == 0)
        {
            return false;
        }
        int leftNumOfPath = pathPointsToMoveon_.Length - numberOfStepsAlreadyMoved;
        if (leftNumOfPath >= numberOfStepsToMove)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
