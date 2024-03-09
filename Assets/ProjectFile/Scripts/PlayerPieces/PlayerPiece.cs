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

    // Pathpoint variable to store the data of pawns like pawns are in which pathpoint
    public PathPoint previousPathPoint;
    public PathPoint currentPathPoint;

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

        // will store the pawn data in pathpoints
        previousPathPoint = pathPointsToMoveon_[0];
        currentPathPoint = pathPointsToMoveon_[0];
        currentPathPoint.AddPlayerPiece(this);
        GameManager.Instance.AddPathPoint(currentPathPoint);

        // Dice managment 
        GameManager.Instance.canDiceRoll = true;
        GameManager.Instance.selfDice = true;
        GameManager.Instance.transferDice = false;
    }
    IEnumerator MoveSteps_Enum(PathPoint[] pathPointsToMoveon_)
    {
        yield return new WaitForSeconds(0.25f);
        // Accroding to the dice we need to move the pawn
        numberOfStepsToMove = GameManager.Instance.numberOfStepsToMove;

        for (int i = numberOfStepsAlreadyMoved; i < (numberOfStepsAlreadyMoved + numberOfStepsToMove); i++)
        {
            currentPathPoint.RescaleAndRepositionAllPlayerPiece();
            if (isPathPointsAvailableToMove(numberOfStepsToMove, numberOfStepsAlreadyMoved, pathPointsToMoveon_))
            {
                transform.position = pathPointsToMoveon_[i].transform.position;

                GameManager.Instance.pawnMovementSound.Play();

                yield return new WaitForSeconds(0.35f);
            }
        }

        if (isPathPointsAvailableToMove(numberOfStepsToMove, numberOfStepsAlreadyMoved, pathPointsToMoveon_))
        {
            GameManager.Instance.transferDice = false;
            numberOfStepsAlreadyMoved += numberOfStepsToMove;

            // will remove the current pathpoint from the list 
            GameManager.Instance.RemovePathPoints(previousPathPoint);
            previousPathPoint.RemovePlayerPiece(this);
            currentPathPoint = pathPointsToMoveon_[numberOfStepsAlreadyMoved - 1];

            // add the new pathpoint 
            if(currentPathPoint.AddPlayerPiece(this))
            {
                if(numberOfStepsAlreadyMoved == 57)
                {
                    GameManager.Instance.selfDice = true;
                }
                else
                {
                    if (GameManager.Instance.numberOfStepsToMove != 6)
                    {
                        GameManager.Instance.selfDice = false;
                        GameManager.Instance.transferDice = true;
                    }
                    else
                    {
                        GameManager.Instance.selfDice = true;
                        GameManager.Instance.transferDice = false;
                    }
                }
            }
            else
            {
                GameManager.Instance.selfDice = true;
            }


            GameManager.Instance.AddPathPoint(currentPathPoint);
            previousPathPoint = currentPathPoint; // To store the current pathpont in order to update in the next dice roll

            GameManager.Instance.numberOfStepsToMove = 0;
        }

        GameManager.Instance.canPlayerMove = true;

        GameManager.Instance.RollingDiceManager();

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
