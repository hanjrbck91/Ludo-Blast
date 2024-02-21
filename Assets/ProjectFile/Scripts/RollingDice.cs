using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingDice : MonoBehaviour
{
    [SerializeField] Sprite[] numberSprites;
    [SerializeField] SpriteRenderer numberSpriteHolder;
    [SerializeField] SpriteRenderer rollingDiceAnimation;
    [SerializeField] int numberGot;

    Coroutine generateRandomNumOnDice;
    public bool canDiceRoll = true;


    public void OnMouseDown()
    {
        generateRandomNumOnDice = StartCoroutine(RollingTheDice());
        Debug.Log("mouse button is clicked");
    }

    IEnumerator RollingTheDice()
    {
        yield return new WaitForEndOfFrame();

        if(canDiceRoll)
        {
            canDiceRoll = false;
            numberSpriteHolder.gameObject.SetActive(false);
            rollingDiceAnimation.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            numberGot = Random.Range(0, 6);
            numberSpriteHolder.sprite = numberSprites[numberGot];
            numberGot += 1;

            // Adding number of steps to move into a variable 
            GameManager.Instance.numberOfStepsToMove = numberGot;

            numberSpriteHolder.gameObject.SetActive(true);
            rollingDiceAnimation.gameObject.SetActive(false);

            yield return new WaitForEndOfFrame();

            canDiceRoll = true;

            if(!System.Object.ReferenceEquals(generateRandomNumOnDice,null))
            {
                StopCoroutine(RollingTheDice());
            }
        }

    }
}
 