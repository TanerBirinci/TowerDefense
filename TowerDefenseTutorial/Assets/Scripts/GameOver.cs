using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text roundsText;


    private void OnEnable()
    {
        roundsText.text = PlayerStats.rounds.ToString();
    }

    public void ReTry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Menu()
    {
        Debug.Log("go menu");
    }
    
}
