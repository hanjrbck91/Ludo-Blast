using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public static Dice Instance;
    AudioSource diceSound;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        diceSound = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        diceSound.Play();
    }
}
