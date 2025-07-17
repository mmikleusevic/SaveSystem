using System;
using TMPro;
using UnityEngine;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void SetHighScore(string username, int score)
    {
        gameObject.SetActive(true);
        
        usernameText.text = $"Username: {username}";
        scoreText.text = $"Score: {score}";
    }
}
