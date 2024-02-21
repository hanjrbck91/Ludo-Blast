using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance;
    public int numberOfStepsToMove;

    private void Awake()
    {
        Instance = this;
    }
}
