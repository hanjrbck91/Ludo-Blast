using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    public PathObjectParent pathObjectParent;
    public List<PlayerPiece> PlayerPieceList = new List<PlayerPiece>();

    private void Start()
    {
        pathObjectParent = FindObjectOfType<PathObjectParent>();
    }

    public void AddPlayerPiece(PlayerPiece playerPiece)
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
