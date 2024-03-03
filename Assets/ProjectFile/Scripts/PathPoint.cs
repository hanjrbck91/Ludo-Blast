using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    PathPoint[] pathPointsToMoveon;
    public PathObjectParent pathObjectParent;
    public List<PlayerPiece> PlayerPieceList = new List<PlayerPiece>();

    private void Start()
    {
        pathObjectParent = FindObjectOfType<PathObjectParent>();
    }

    public bool AddPlayerPiece(PlayerPiece playerPiece)
    {
        if(this.name == "PathPoint_5")
        {
            PathCompleted(playerPiece);
        }

        if (this.name != "PathPoint" && this.name != "PathPoint_8" && this.name != "PathPoint_13" && this.name != "PathPoint_21"
            && this.name != "PathPoint_26" && this.name != "PathPoint_34" && this.name != "PathPoint_39" && this.name != "PathPoint_47")
        {
            if (PlayerPieceList.Count == 1)
            {
                string previousPlayerPieceName = PlayerPieceList[0].name;
                string currentPlayerPieceName = playerPiece.name;
                currentPlayerPieceName = currentPlayerPieceName.Substring(0, currentPlayerPieceName.Length - 4);

                if (!previousPlayerPieceName.Contains(currentPlayerPieceName))
                {
                    PlayerPieceList[0].isReady = false;

                    StartCoroutine(revertPlayerPieceToStart(PlayerPieceList[0]));

                    PlayerPieceList[0].numberOfStepsAlreadyMoved = 0;
                    RemovePlayerPiece(PlayerPieceList[0]);
                    PlayerPieceList.Add(playerPiece);

                    return false;
                }
            }
        }

        addPlayer(playerPiece);

        return true;
    }

    private void PathCompleted(PlayerPiece playerPiece)
    {
        if (playerPiece.name.Contains("Blue")) { GameManager.Instance.blueCompletePlayers += 1; GameManager.Instance.blueOutPlayers -= 1;  }
        else if (playerPiece.name.Contains("Red")) { GameManager.Instance.redCompletePlayers += 1; GameManager.Instance.redOutPlayers -= 1;  }
        else if (playerPiece.name.Contains("Green")) { GameManager.Instance.greenCompletePlayers += 1; GameManager.Instance.greenOutPlayers -= 1;  }
        else if (playerPiece.name.Contains("Yellow")) { GameManager.Instance.yellowCompletePlayers += 1; GameManager.Instance.yellowOutPlayers -= 1; }
    }

    private IEnumerator revertPlayerPieceToStart(PlayerPiece playerPiece)
    {
        if (playerPiece.name.Contains("Blue")) { GameManager.Instance.blueOutPlayers -= 1; pathPointsToMoveon = pathObjectParent.bluePathPoint; if (GameManager.Instance.blueCompletePlayers == 4) { ShowWinningCelebration(); } }
        else if (playerPiece.name.Contains("Red")) { GameManager.Instance.redOutPlayers -= 1; pathPointsToMoveon = pathObjectParent.redPathPoint; if (GameManager.Instance.redCompletePlayers == 4) { ShowWinningCelebration(); } }
        else if (playerPiece.name.Contains("Green")) { GameManager.Instance.greenOutPlayers -= 1; pathPointsToMoveon = pathObjectParent.greenPathPoint; if (GameManager.Instance.greenCompletePlayers == 4) { ShowWinningCelebration(); } }
        else if (playerPiece.name.Contains("Yellow")) { GameManager.Instance.yellowOutPlayers -= 1; pathPointsToMoveon = pathObjectParent.yellowPathPoint; if (GameManager.Instance.yellowCompletePlayers == 4) { ShowWinningCelebration(); }  }

        for (int i = playerPiece.numberOfStepsAlreadyMoved-1; i >= 0; i--)
        {
            playerPiece.transform.position = pathPointsToMoveon[i].transform.position;

            yield return new WaitForSeconds(0.1f);
        }

        playerPiece.transform.position = pathObjectParent.pawnHomePathPoints[BasePointPosition(playerPiece.name)].transform.position;
    }

    private void ShowWinningCelebration()
    {
        throw new NotImplementedException();
    }

    int BasePointPosition(string name)
    {
        for (int i = 0; i < pathObjectParent.pawnHomePathPoints.Length; i++)
        {
            if (pathObjectParent.pawnHomePathPoints[i].name == name)
            {
                return i;
            }
        }

        return -1;
    }

    void addPlayer(PlayerPiece playerPiece)
    {
        PlayerPieceList.Add(playerPiece);
        RescaleAndRepositionAllPlayerPiece();
    }

    public void RemovePlayerPiece(PlayerPiece playerPiece)
    {
        if(PlayerPieceList.Contains(playerPiece))
        {
            PlayerPieceList.Remove(playerPiece);
            RescaleAndRepositionAllPlayerPiece();
        }
    }
            

    public void RescaleAndRepositionAllPlayerPiece()
    {
        int pieceCount = PlayerPieceList.Count;
        bool isOdd = (pieceCount % 2) == 0 ? false : true;

        int extent = pieceCount / 2;
        int counter = 0;
        int spriteLayer = 1;

        if(isOdd)
        {
            for(int i = -extent; i <= extent; i++)
            {
                Vector3 localScale = new Vector3(pathObjectParent.scales[pieceCount - 1], pathObjectParent.scales[pieceCount - 1], 1f);
                Vector3 localPos = new Vector3(transform.position.x + (i * pathObjectParent.positionDifference[pieceCount - 1]), transform.position.y, 0f);

                PlayerPieceList[counter].transform.localScale = localScale;
                PlayerPieceList[counter].transform.position = localPos;
                counter++;
            }
        }
        else
        {
            for (int i = -extent; i < extent; i++)
            {
                Vector3 localScale = new Vector3(pathObjectParent.scales[pieceCount - 1], pathObjectParent.scales[pieceCount - 1], 1f);
                Vector3 localPos = new Vector3(transform.position.x + (i * pathObjectParent.positionDifference[pieceCount - 1]), transform.position.y, 0f);

                PlayerPieceList[counter].transform.localScale = localScale;
                PlayerPieceList[counter].transform.position = localPos;
                counter++;
            }
        }
        for(int i = 0; i < PlayerPieceList.Count; i++)
        {
            PlayerPieceList[i].GetComponent<SpriteRenderer>().sortingOrder = spriteLayer;
            spriteLayer++;
        }
    }
}
