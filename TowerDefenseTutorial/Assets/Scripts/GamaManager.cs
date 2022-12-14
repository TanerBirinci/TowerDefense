using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public static bool gameIsOver = false;

    private void Start()
    {
        gameIsOver = false;
    }

    void Update()
    {
        if (gameIsOver)
        {
            return;
        }

        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }
        
        if (PlayerStats.Lives<=0)
        {
            EndGame();
        } 
    }

    void EndGame()
    {
        gameIsOver = true;
        Debug.Log("Game Over");
        gameOverUI.SetActive(true);
    }
}
