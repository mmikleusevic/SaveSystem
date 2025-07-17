using System;
using UnityEngine;

[Serializable]
public class HighScore
{
    [SerializeField] private string username;
    [SerializeField] private int score;
    
    public string Username => username;
    public int Score => score;

    public HighScore(string username, int score)
    {
        this.username = username;
        this.score = score;
    }
}