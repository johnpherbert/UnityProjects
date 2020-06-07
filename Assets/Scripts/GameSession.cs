﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private int score = 0;

    // Start is called before the first frame update
    void Awake()
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
