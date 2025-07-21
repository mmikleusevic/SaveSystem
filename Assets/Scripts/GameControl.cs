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
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private GameObject coinPrefab;
    
    private int score;
    private List<HighScore> highScores;
    private int highScore;
    private Vector3[] coinPositions;

    private void Start()
    {
        score = 0;
        scoreText.text = $"Score: {score}";
        
        highScore = JsonSave.Instance.GetScore();
        
        highscoreText.text = $"Highscore: {highScore}";
        //GetHighScores();
        //UpdateUI();

        InstantiateCoins();
        
        transform.position = JsonSave.Instance.GetPosition();
    }

    private void InstantiateCoins()
    {
        coinPositions = JsonSave.Instance.GetPositions();

        if (coinPositions.Length == 0)
        {
            coinPositions = new Vector3[4];
            coinPositions[0] = new Vector3(0,0,0);
            coinPositions[1] = new Vector3(1,0,0);
            coinPositions[2] = new Vector3(0,1,0);
            coinPositions[3] = new Vector3(0,0,1);

            SetPositions();
        }
        
        foreach (Vector3 position in coinPositions)
        {
            Instantiate(coinPrefab, position, Quaternion.identity);
        }
    }

    public void SetPositions()
    {
        JsonSave.Instance.SetPositions(coinPositions);
    }

    public void ChangeScore(int scoreAmount)
    {
        score += scoreAmount;
        scoreText.text = $"Score: {score}";

        if (score > highScore)
        {
            highScore = score;
            highscoreText.text = $"Highscore: {highScore}";
            JsonSave.Instance.SetScore(highScore);
            JsonSave.Instance.SetPosition(transform.position);
        }
    }

    public void Save()
    {
        // if (highScores.Count == 0 || highScores.Any(a => a.Score < score))
        // {
        //     highScores.Add(new HighScore(inputField.text, score));
        //
        //     SaveHighScores();
        //     UpdateUI();
        // }
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

    public void SetName(string playerName)
    {
        playerNameText.text = playerName;
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