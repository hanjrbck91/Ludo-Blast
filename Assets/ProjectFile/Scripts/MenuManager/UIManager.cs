using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject GamePanel;

    public void PlayTwo()
    {
        GameManager.Instance.totalPlayerCanPlay = 2;
        MainPanel.SetActive(false);
        GamePanel.SetActive(true);
        PlayerTwoSetting();
        Debug.Log("hi");
    }

    public void PlayThree()
    {
        GameManager.Instance.totalPlayerCanPlay = 3;
        MainPanel.SetActive(false);
        GamePanel.SetActive(true);
        PlayerThreeSetting();
    }

    public void PlayFour()
    {
        GameManager.Instance.totalPlayerCanPlay = 4;
        MainPanel.SetActive(false);
        GamePanel.SetActive(true);
    }

    public void PlayComputer()
    {
        GameManager.Instance.totalPlayerCanPlay = 1;
        MainPanel.SetActive(false);
        GamePanel.SetActive(true);
        PlayerTwoSetting();
    }

    void PlayerTwoSetting()
    {
        HidePlayers(GameManager.Instance.redPlayerPiece);
        HidePlayers(GameManager.Instance.yellowPlayerPiece);
    }

    void PlayerThreeSetting()
    {
        HidePlayers(GameManager.Instance.yellowPlayerPiece);
    }

    private void HidePlayers(PlayerPiece[] PlayerPiece)
    {
        for (int i = 0; i < PlayerPiece.Length; i++)
        {
            PlayerPiece[i].gameObject.SetActive(false);
        }
    }
}
