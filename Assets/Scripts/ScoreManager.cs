using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;

    public uint Score { get; private set; } = 0;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = GameObject.FindObjectOfType<ScoreManager>();
            if (_instance == null)
                Debug.Log("Add a score manager some where dude. im not your mom");

            return _instance;
        }
        private set => _instance = value;
    }


    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void AddScore(uint add)
    {
        Score += add;
    }
}