using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    public PathObjectParent pathObjectParent;
    public List<PlayerPiece> PlayerPieceList = new List<PlayerPiece>();

    private void Start()
    {
        pathObjectParent = FindObjectOfType<PathObjectParent>();
    }

    public bool AddPlayerPiece(PlayerPiece playerPiece)
    {
        if(PlayerPieceList.Count == 1)
        {
            string previousPlayerPieceName = PlayerPieceList[0].name;
            string currentPlayerPieceName = playerPiece.name;
            currentPlayerPieceName = currentPlayerPieceName.Substring(0, currentPlayerPieceName.Length - 4);

            if (!previousPlayerPieceName.Contains(currentPlayerPieceName))
            {
                PlayerPieceList[0].isReady = false;

                revertPlayerPieceToStart(PlayerPieceList[0]);

                PlayerPieceList[0].numberOfStepsAlreadyMoved = 0;
                RemovePlayerPiece(PlayerPieceList[0]);
                PlayerPieceList.Add(playerPiece);

                return false;
            }
        }

        
        addPlayer(playerPiece);

        return true;
    }

    private void revertPlayerPieceToStart(PlayerPiece playerPiece)
    {


        PlayerPieceList[0].transform.position = pathObjectParent.pawnHomePathPoints[BasePointPosition(playerPiece.name)].transform.position;
    }

    int BasePointPosition(string name)
    {
        if (name.Contains("Blue")) { GameManager.Instance.blueOutPlayers -= 1; }
        else if (name.Contains("Red")) { GameManager.Instance.redOutPlayers -= 1; }
        else if (name.Contains("Green")) { GameManager.Instance.greenOutPlayers -= 1; }
        else if (name.Contains("Yellow")) { GameManager.Instance.yellowOutPlayers -= 1; }

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
