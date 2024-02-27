using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RollingDice : MonoBehaviour
{
    [SerializeField] Sprite[] numberSprites;
    [SerializeField] SpriteRenderer numberSpriteHolder;
    [SerializeField] SpriteRenderer rollingDiceAnimation;
    [SerializeField] int numberGot;

    Coroutine generateRandomNumOnDice;
    // variable to store the outpieces of which color when dice rolled
    public int outPieces;


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

            numberGot = Random.Range(0, 6);
            numberSpriteHolder.sprite = numberSprites[numberGot];
            numberGot += 1;

            // Adding number of steps to move into a variable 
            GameManager.Instance.numberOfStepsToMove = numberGot;
            //Setting up the dice of specific pawn(player)
            GameManager.Instance.rollingDice = this;

            numberSpriteHolder.gameObject.SetActive(true);
            rollingDiceAnimation.gameObject.SetActive(false);

            yield return new WaitForEndOfFrame();

            if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[0]) { outPieces = GameManager.Instance.blueOutPlayers; }
            else if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[1]) { outPieces = GameManager.Instance.redOutPlayers; }
            else if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[2]) { outPieces = GameManager.Instance.greenOutPlayers; }
            else if (GameManager.Instance.rollingDice == GameManager.Instance.manageRollingDice[3]) { outPieces = GameManager.Instance.yellowOutPlayers; }

            //GameManager.Instance.canDiceRoll = true;
            if (GameManager.Instance.numberOfStepsToMove != 6 && outPieces == 0)
            {
                GameManager.Instance.canDiceRoll = true;
                GameManager.Instance.selfDice = false;
                GameManager.Instance.transferDice = true;

                yield return new WaitForSeconds(0.5f);
                GameManager.Instance.RollingDiceManager();
            }

            if (!System.Object.ReferenceEquals(generateRandomNumOnDice,null))
            {
                StopCoroutine(RollingTheDice());
            }
        }

    }
}
 