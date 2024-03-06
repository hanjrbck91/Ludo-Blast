using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RollingDice : MonoBehaviour
{
    [SerializeField] Sprite[] numberSprites;
    [SerializeField] SpriteRenderer numberSpriteHolder;
    [SerializeField] SpriteRenderer rollingDiceAnimation;
    [SerializeField] int numberDiceGot;

    Coroutine generateRandomNumOnDice;
    // variable to store the outpieces of which color when dice rolled
    public int outPieces;

    public PathObjectParent pathParent;
    PlayerPiece[] currentPlayerPiece;
    PathPoint[] pathPointToMoveOn;
    Coroutine MovePlayerPiece;
    PlayerPiece outPlayerPiece;

    private void Awake()
    {
        pathParent = FindObjectOfType<PathObjectParent>();
    }


    public void OnMouseDown()
    {
        generateRandomNumOnDice = StartCoroutine(RollingTheDice());
        Debug.Log("mouse button is clicked");
    }

    IEnumerator RollingTheDice()
    {
        yield return new WaitForEndOfFrame();

        if(GameManager.Instance.canDiceRoll)
        {
            GameManager.Instance.canDiceRoll = false;
            numberSpriteHolder.gameObject.SetActive(false);
            rollingDiceAnimation.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            numberDiceGot = Random.Range(0, 6);
            numberSpriteHolder.sprite = numberSprites[numberDiceGot];
            numberDiceGot += 1;

            // Adding number of steps to move into a variable 
            GameManager.Instance.numberOfStepsToMove = numberDiceGot;
            //Setting up the dice of specific pawn(player)
            GameManager.Instance.rollingDice = this;

            numberSpriteHolder.gameObject.SetActive(true);
            rollingDiceAnimation.gameObject.SetActive(false);

            yield return new WaitForEndOfFrame();

            int numberGot = GameManager.Instance.numberOfStepsToMove;
            if (PlayerCanNotMove())
            {
                yield return new WaitForSeconds(0.5f);

                if (numberGot != 6) { GameManager.Instance.transferDice = true; }
                else { GameManager.Instance.selfDice = true; }
            }
            else
            {
                if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[0]) { outPieces = GameManager.Instance.blueOutPlayers; }
                else if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[1]) { outPieces = GameManager.Instance.redOutPlayers; }
                else if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[2]) { outPieces = GameManager.Instance.greenOutPlayers; }
                else if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[3]) { outPieces = GameManager.Instance.yellowOutPlayers; }

                //GameManager.Instance.canDiceRoll = true;
                if (outPieces == 0 && numberGot != 6)
                {
                    yield return new WaitForSeconds(0.5f);
                    GameManager.Instance.transferDice = true;
                }
                else
                {
                    if (outPieces == 0 && numberGot == 6)
                    {
                        MakePlayerReadyToMove(0);
                    }
                    else if(outPieces == 1 && numberGot != 6 && GameManager.Instance.canPlayerMove)
                    {
                        GameManager.Instance.canPlayerMove = false;
                        MovePlayerPiece = StartCoroutine(MoveSteps_Enum(0));
                    }
                }
            }

            GameManager.Instance.RollingDiceManager();

            if (!System.Object.ReferenceEquals(generateRandomNumOnDice,null))
            {
                StopCoroutine(RollingTheDice());
            }
        }

    }

    public bool PlayerCanNotMove()
    {
        if(outPieces > 0)
        {
            bool canNotMove = false;

            if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[0]) { currentPlayerPiece = GameManager.Instance.bluePlayerPiece; pathPointToMoveOn = pathParent.bluePathPoint; }
            else if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[1]) { currentPlayerPiece = GameManager.Instance.bluePlayerPiece; pathPointToMoveOn = pathParent.bluePathPoint; }
            else if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[2]) { currentPlayerPiece = GameManager.Instance.bluePlayerPiece; pathPointToMoveOn = pathParent.bluePathPoint; }
            else if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[3]) { currentPlayerPiece = GameManager.Instance.bluePlayerPiece; pathPointToMoveOn = pathParent.bluePathPoint; }
        
            for(int i = 0; i < currentPlayerPiece.Length; i++)
            {
                if (currentPlayerPiece[i].isReady)
                {
                    if (isPathPointsAvailableToMove(GameManager.Instance.numberOfStepsToMove, currentPlayerPiece[i].numberOfStepsAlreadyMoved, pathPointToMoveOn))
                    {
                        return false;
                    }
                }
                else
                {
                    if(!canNotMove)
                    {
                        canNotMove = true;
                    }
                }
            }
            if(canNotMove) { return true; }
        }

        return false;
    }

    bool isPathPointsAvailableToMove(int numberOfStepsToMove, int numberOfStepsAlreadyMoved, PathPoint[] pathPointsToMoveon_)
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

    public void MakePlayerReadyToMove(int outPlayer)
    {
        if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[0]) { outPlayerPiece = GameManager.Instance.bluePlayerPiece[outPlayer]; pathPointToMoveOn = pathParent.bluePathPoint; GameManager.Instance.blueOutPlayers += 1; }
        else if(GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[1]) { outPlayerPiece = GameManager.Instance.redPlayerPiece[outPlayer]; pathPointToMoveOn = pathParent.redPathPoint; GameManager.Instance.redOutPlayers += 1; }
        else if(GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[2]) { outPlayerPiece = GameManager.Instance.greenPlayerPiece[outPlayer]; pathPointToMoveOn = pathParent.greenPathPoint; GameManager.Instance.greenOutPlayers += 1; }
        else if(GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[3]) { outPlayerPiece = GameManager.Instance.yellowPlayerPiece[outPlayer]; pathPointToMoveOn = pathParent.yellowPathPoint; GameManager.Instance.yellowOutPlayers += 1; }


        outPlayerPiece.isReady = true;
        outPlayerPiece.transform.position = pathPointToMoveOn[0].transform.position;
        outPlayerPiece.numberOfStepsAlreadyMoved = 1;

        // will store the pawn data in pathpoints
        outPlayerPiece.previousPathPoint = pathPointToMoveOn[0];
        outPlayerPiece.currentPathPoint = pathPointToMoveOn[0];
        outPlayerPiece.currentPathPoint.AddPlayerPiece(outPlayerPiece);
        GameManager.Instance.AddPathPoint(outPlayerPiece.currentPathPoint);  

        // Dice managment 
        GameManager.Instance.canDiceRoll = true;
        GameManager.Instance.selfDice = true;
        GameManager.Instance.transferDice = false;
        GameManager.Instance.numberOfStepsToMove = 0;
    }

    IEnumerator MoveSteps_Enum(int movePlayer)
    {

        if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[0]) { outPlayerPiece = GameManager.Instance.bluePlayerPiece[movePlayer]; pathPointToMoveOn = pathParent.bluePathPoint;}
        else if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[1]) { outPlayerPiece = GameManager.Instance.redPlayerPiece[movePlayer]; pathPointToMoveOn = pathParent.redPathPoint;}
        else if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[2]) { outPlayerPiece = GameManager.Instance.greenPlayerPiece[movePlayer]; pathPointToMoveOn = pathParent.greenPathPoint;}
        else if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[3]) { outPlayerPiece = GameManager.Instance.yellowPlayerPiece[movePlayer]; pathPointToMoveOn = pathParent.yellowPathPoint;}

        GameManager.Instance.transferDice = false;
        yield return new WaitForSeconds(0.25f);
        // Accroding to the dice we need to move the pawn
        int numberOfStepsToMove = GameManager.Instance.numberOfStepsToMove;
        outPlayerPiece.currentPathPoint.RescaleAndRepositionAllPlayerPiece();

        for (int i = outPlayerPiece.numberOfStepsAlreadyMoved; i < (outPlayerPiece.numberOfStepsAlreadyMoved + numberOfStepsToMove); i++)
        {
            if (isPathPointsAvailableToMove(numberOfStepsToMove, outPlayerPiece.numberOfStepsAlreadyMoved, pathPointToMoveOn))
            {
                outPlayerPiece.transform.position = pathPointToMoveOn[i].transform.position;

                yield return new WaitForSeconds(0.35f);
            }
        }

        if (isPathPointsAvailableToMove(numberOfStepsToMove, outPlayerPiece.numberOfStepsAlreadyMoved, pathPointToMoveOn))
        {
            outPlayerPiece.numberOfStepsAlreadyMoved += numberOfStepsToMove;

            // will remove the current pathpoint from the list 
            GameManager.Instance.RemovePathPoints(outPlayerPiece.previousPathPoint);
            outPlayerPiece.previousPathPoint.RemovePlayerPiece(outPlayerPiece);
            outPlayerPiece.currentPathPoint = pathPointToMoveOn[outPlayerPiece.numberOfStepsAlreadyMoved - 1];


            // add the new pathpoint 
            if (outPlayerPiece.currentPathPoint.AddPlayerPiece(outPlayerPiece))
            {
                if (outPlayerPiece.numberOfStepsAlreadyMoved == 57)
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


            GameManager.Instance.AddPathPoint(outPlayerPiece.currentPathPoint);
            outPlayerPiece.previousPathPoint = outPlayerPiece.currentPathPoint; // To store the current pathpont in order to update in the next dice roll

            GameManager.Instance.numberOfStepsToMove = 0;
        }

        GameManager.Instance.canPlayerMove = true;

        GameManager.Instance.RollingDiceManager();

        if (!System.Object.ReferenceEquals(MovePlayerPiece, null))
        {
            StopCoroutine("MoveSteps_Enum");
        }
    }

}
