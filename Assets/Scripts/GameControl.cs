using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using File = System.IO.File;

public class GameControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private HighScoreUI highscoreTemplate;
    [SerializeField] private GameObject highscoreContainer;
    
    private int score;
    private List<HighScore> highScores;

    private void Start()
    {
        score = 0;
        scoreText.text = $"Score: {score}";
        
        GetHighScores();
        UpdateUI();
    }

    public void ChangeScore(int scoreAmount)
    {
        score += scoreAmount;
        scoreText.text = $"Score: {score}";
    }

    public void Save()
    {
        if (highScores.Count == 0 || highScores.Any(a => a.Score < score))
        {
            highScores.Add(new HighScore(inputField.text, score));

            SaveHighScores();
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < highscoreContainer.transform.childCount; i++)
        {
            if (i == 0) continue;
            Destroy(highscoreContainer.transform.GetChild(i).gameObject);
        }

        highScores = highScores.OrderByDescending(h => h.Score).ToList();
        
        foreach (HighScore highscore in highScores)
        {
            HighScoreUI highScoreUI = Instantiate(highscoreTemplate, highscoreContainer.transform);
            highScoreUI.SetHighScore(highscore.Username, highscore.Score);
        }
    }

    private void SaveHighScores()
    {
        HighScoreList wrapper = new HighScoreList { scores = highScores };
        string highScoresJSON = JsonUtility.ToJson(wrapper);
        File.WriteAllText(Application.dataPath + "/" + Tags.HIGHSCORES, highScoresJSON);
    }

    private void GetHighScores()
    {
        string path = Application.dataPath + "/" + Tags.HIGHSCORES;

        if (!File.Exists(path))
        {
            highScores = new List<HighScore>();
            return;
        }

        string highscoresJSON = File.ReadAllText(path);

        HighScoreList wrapper = JsonUtility.FromJson<HighScoreList>(highscoresJSON);
        highScores = wrapper.scores;

        UpdateUI();
    }
}